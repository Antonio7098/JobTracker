using JobTracker.Api.Models;
using JobTracker.Api.Services;
using JobTracker.Api.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Threading.Tasks;

namespace JobTracker.Api.Test.Repositories;

public class MySqlEmployersRepositoryTests : IDisposable
{
    private readonly JobTrackerDbContext _context;
    private readonly IEmployersRepository _repository;

    public MySqlEmployersRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<JobTrackerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new JobTrackerDbContext(options);
        _repository = new MySqlEmployersRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task GetAllEmployers_WhenDatabaseIsEmpty_ReturnsEmptyList()
    {
        var employers = await _repository.GetAllEmployers();
        Assert.Empty(employers);   
    }

    [Fact]
    public async Task CreateEmployer_WhenValidEmployer_AddsToDatabase()
    {
        var newEmployer = new Employer{
            Name = "Test Employer",
            CompanyDescription = "Description"
        };

        var createdEmployer = await _repository.CreateEmployer(newEmployer);

        Assert.NotNull(createdEmployer);

        var employerFromDb = await _context.Employers.FindAsync(createdEmployer.Id);

        Assert.NotNull(employerFromDb);
        Assert.Equal("Test Employer", employerFromDb.Name);
        Assert.Equal("Description", employerFromDb.CompanyDescription);
    }

    [Fact]
    public async Task GetEmployerById_WhenEmployerExists_ReturnsEmployer()
    {
        var newEmployer = new Employer{
            Id = Guid.NewGuid(),
            Name = "Test Employer",
            CompanyDescription = "Description"
        };
        
        await _context.Employers.AddAsync(newEmployer);
        await _context.SaveChangesAsync();

        var employerFromDb = await _repository.GetEmployerById(newEmployer.Id);

        Assert.NotNull(employerFromDb);
        Assert.Equal(newEmployer.Name, employerFromDb.Name);
        Assert.Equal(newEmployer.CompanyDescription, employerFromDb.CompanyDescription);
        Assert.Equal(newEmployer.Id, employerFromDb.Id);
    }

    [Fact]
    public async Task UpdateEmployerById_WhenEmployerExists_UpdatesEmployer()
    {
        var newEmployer = new Employer{
            Id = Guid.NewGuid(),
            Name = "Test Employer",
            CompanyDescription = "Description"
        };

        await _context.Employers.AddAsync(newEmployer);
        await _context.SaveChangesAsync();

        var updatedEmployer = new Employer{
            Id = newEmployer.Id,
            Name = "Test Employer Updated",
            CompanyDescription = "Description updated"
        };

        var result = await _repository.UpdateEmployer(newEmployer.Id, updatedEmployer);

        Assert.True(result);

        var updatedEmployerFromDb = await _repository.GetEmployerById(newEmployer.Id);

        Assert.NotNull(updatedEmployerFromDb);
        Assert.Equal(updatedEmployer.Name, updatedEmployerFromDb.Name);
        Assert.Equal(updatedEmployer.CompanyDescription, updatedEmployerFromDb.CompanyDescription);
    }

    [Fact]
    public async Task DeleteEmployer_WhenEmployerExists_RemovesEmployer()
    {
        var newEmployer = new Employer{
            Id = Guid.NewGuid(),
            Name = "Test Employer",
            CompanyDescription = "Description"
        };

        await _context.Employers.AddAsync(newEmployer);
        await _context.SaveChangesAsync();

        var result = await _repository.DeleteEmployer(newEmployer.Id);

        Assert.True(result);

        var employerFromDb = await _repository.GetEmployerById(newEmployer.Id);

        Assert.Null(employerFromDb);
    }

    [Fact]
    public async Task GetEmployerById_WhenEmployerDoesNotExist_ReturnsNull()
    {
        var employer = await _repository.GetEmployerById(Guid.NewGuid());

        Assert.Null(employer);
    }

    [Fact]
    public async Task UpdateEmployerById_WhenEmployerDoesNotExist_ReturnsFalse()
    {
        var nonExistentEmployer = new Employer{
            Id = Guid.NewGuid(),
            Name = "Test Employer",
            CompanyDescription = "Description"
        };

        var result = await _repository.UpdateEmployer(nonExistentEmployer.Id, nonExistentEmployer);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteEmployer_WhenEmployerDoesNotExist_ReturnsFalse()
    {
        var result = await _repository.DeleteEmployer(Guid.NewGuid());

        Assert.False(result);
    }
}