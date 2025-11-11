using System;
using JobTracker.Api.DTOs;
using JobTracker.Api.Models;

namespace JobTracker.Api.Services;

public interface IEmployersRepository
{
    Task<IEnumerable<Employer>> GetAllEmployers();
    Task<Employer?> GetEmployerById(Guid id);
    Task CreateEmployer(Employer employer);
    Task<bool> UpdateEmployer(Guid id, UpdateEmployerDto UpdatedEmployer);
    Task<bool> DeleteEmployer(Guid id);
}
