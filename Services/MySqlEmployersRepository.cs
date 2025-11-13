using System;
using JobTracker.Api.Services;
using JobTracker.Api.Models;
using JobTracker.Api.Data;
using JobTracker.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Api.Services;

public class MySqlEmployersRepository : IEmployersRepository
{
    private readonly JobTrackerDbContext _dbContext;

    public MySqlEmployersRepository(JobTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Employer>> GetAllEmployers()
    {
        return await _dbContext.Employers.ToListAsync();
    }

    public async Task<Employer?> GetEmployerById(Guid id)
    {
        return await _dbContext.Employers.FirstOrDefaultAsync(employer => employer.Id == id);
    }

    public async Task<Employer> CreateEmployer(Employer newEmployer)
    {
        await _dbContext.Employers.AddAsync(newEmployer);
        await _dbContext.SaveChangesAsync();
        return newEmployer;
    }

    public async Task<bool> UpdateEmployer(Guid id, Employer updatedEmployer)
    {
        Employer? employer = await _dbContext.Employers.FirstOrDefaultAsync(employer => employer.Id == id);

        if (employer is null)
        {
            return false;
        }

        employer.Name = updatedEmployer.Name;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteEmployer(Guid id)
    {
        Employer? employer = await _dbContext.Employers.FirstOrDefaultAsync(employer => employer.Id == id);

        if (employer is null)
        {
            return false;
        }
        
        _dbContext.Employers.Remove(employer);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}