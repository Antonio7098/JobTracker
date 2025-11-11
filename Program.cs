using JobTracker.Api.Models;
using JobTracker.Api.Services;
using JobTracker.Api.Endpoints;
using JobTracker.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployersRepository, InMemoryEmployersRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultCOnnection")
builder.Services.AddDbContext<JobTrackerDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

app.MapEmployersEndpoints();

app.Run();