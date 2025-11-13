using System;
using System.Linq;
using JobTracker.Api.Services;
using JobTracker.Api.Models;
using JobTracker.Api.DTOs;

namespace JobTracker.Api.Services;

public class InMemoryEmployersRepository : IEmployersRepository
{
    private static readonly List<Employer> _employers = new();

    public Task<Employer> CreateEmployer(Employer employer)
    {
        employer.Id = Guid.NewGuid();
        _employers.Add(employer);

        return Task.FromResult(employer);
    }

    public Task<IEnumerable<Employer>> GetAllEmployers()
    {
        return Task.FromResult<IEnumerable<Employer>>(_employers);
    }

    public Task<Employer?> GetEmployerById(Guid id)
    {
        Employer? employer = _employers.FirstOrDefault(employer => employer.Id == id);

        return Task.FromResult(employer);
    }

    public Task<bool> UpdateEmployer(Guid id, Employer updatedEmployer)
    {
        Employer? employer = _employers.FirstOrDefault(employer => employer.Id == id);

        if (employer is null)
        {
            return Task.FromResult(false);
        }

        employer.Name = updatedEmployer.Name;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteEmployer(Guid id)
    {
        int outcome = _employers.RemoveAll(employer => employer.Id == id);

        if (outcome == 0)
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }
}