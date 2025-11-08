using JobTracker.Api.Models;
using JobTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployersRepository, InMemoryEmployersRepository>();

var app = builder.Build();

app.MapGet("/employers", async (IEmployersRepository repo) =>
{
    return await repo.GetAllEmployers();
});

app.MapGet("/employers/{id}", async (Guid id, IEmployersRepository repo) =>{

    Employer employer = await repo.GetEmployerById(id);

    if (employer is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(employer);
    
});

app.MapPost("/employers", async (Employer employer, IEmployersRepository repo) =>
{
    await repo.CreateEmployer(employer);

    return Results.Created($"/employers/{employer.Id}", employer);
});

app.Run();
