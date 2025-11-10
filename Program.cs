using JobTracker.Api.Models;
using JobTracker.Api.Services;
using JobTracker.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployersRepository, InMemoryEmployersRepository>();

var app = builder.Build();

app.MapEmployersEndpoints();

app.Run();