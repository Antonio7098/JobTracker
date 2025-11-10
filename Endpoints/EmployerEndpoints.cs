using System;
using JobTracker.Api.Models;
using JobTracker.Api.DTOs;
using JobTracker.Api.Services;
using JobTracker.Api.Maps;


namespace JobTracker.Api.Endpoints;

public static class EmployersEndpointsExtensions
{
    public static void MapEmployersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("employers");

        group.MapGet("/", async (IEmployersRepository repo) =>
        {
            var employers = await repo.GetAllEmployers();

            var employerDtos = employers.Select(employer => employer.ToDto());

            return Results.Ok(employerDtos);
        });

        group.MapGet("/{id}", async (Guid id, IEmployersRepository repo) =>
        {

            Employer? employer = await repo.GetEmployerById(id);

            if (employer is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(employer.ToDto());

        });

        group.MapPost("/", async (CreateEmployerDto createEmployerDto, IEmployersRepository repo) =>
        {
            var employer = createEmployerDto.ToEmployer();
            await repo.CreateEmployer(employer);

            return Results.Created($"/employers/{employer.Id}", employer.ToDto());
        });

        group.MapPut("/{id}", async (Guid id, UpdateEmployerDto updateEmployerDto, IEmployersRepository repo) =>
        {
            var wasFound = await repo.UpdateEmployer(id, updateEmployerDto);

            if (wasFound)
            {
                return Results.NoContent();
            }

            return Results.NotFound();
        });
        
        group.MapDelete("/{id}", async (Guid id, IEmployersRepository repo) =>
        {
            var wasFound = await repo.DeleteEmployer(id);

            if (wasFound)
            {
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }

};
