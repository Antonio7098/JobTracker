using System;
using JobTracker.Api.DTOs;
using JobTracker.Api.Models;

namespace JobTracker.Api.Services;

public interface IEmployersRepository
{
    Task<IEnumerable<Employer>> GetAllEmployers();
    Task<Employer?> GetEmployerById(Guid id);
    Task<Employer> CreateEmployer(Employer newEmployer);
    Task<bool> UpdateEmployer(Guid id, Employer updatedEmployer);
    Task<bool> DeleteEmployer(Guid id);
}
