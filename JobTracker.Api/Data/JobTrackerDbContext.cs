using System;
using JobTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Api.Data;

public class JobTrackerDbContext : DbContext
{
    public JobTrackerDbContext(DbContextOptions<JobTrackerDbContext> options) : base(options)
    {
    }
    public DbSet<Employer> Employers {get; set;} = null!;

    public DbSet<JobVacancy> JobVacancies { get; set; } = null!;
}
