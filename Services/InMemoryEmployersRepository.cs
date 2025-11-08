using System;
using System.Linq;
using JobTracker.Api.Services;
using JobTracker.Api.Models;

namespace JobTracker.Api.Services;

public class InMemoryEmployersRepository : IEmployersRepository
{
    private static readonly List<Employer> _employers = new();

    public Task CreateEmployer(Employer employer)
    {
        employer.Id = Guid.NewGuid();
        _employers.Add(employer);

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Employer>> GetAllEmployers()
    {
        return Task.FromResult<IEnumerable<Employer>>(_employers);
    }

    public Task<Employer?> GetEmployerById(Guid id)
    {
        Employer employer = _employers.FirstOrDefault(employer => employer.Id == id);

        return Task.FromResult(employer);
    }


}