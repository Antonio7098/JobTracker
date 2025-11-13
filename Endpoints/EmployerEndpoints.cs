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
        })
        .Produces<IEnumerable<EmployerDto>>()
        .WithSummary("Gets a list of all employers")
        .WithDescription("Retrieves a complete list of all employers currently stored in the database.");

        group.MapGet("/{id}", async (Guid id, IEmployersRepository repo) =>
        {

            Employer? employer = await repo.GetEmployerById(id);

            if (employer is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(employer.ToDto());

        })
        .Produces<EmployerDto>()
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Gets an employer.")
        .WithDescription("Retrieves an employer form the database by its id.");

        group.MapPost("/", async (CreateEmployerDto createEmployerDto, IEmployersRepository repo) =>
        {
            var employer = createEmployerDto.ToEmployer();
            await repo.CreateEmployer(employer);

            return Results.Created($"/employers/{employer.Id}", employer.ToDto());
        })
        .Produces<EmployerDto>(StatusCodes.Status201Created)
        .WithSummary("Adds a new employer")
        .WithDescription("Adds a new employer to the database.");

        group.MapPut("/{id}", async (Guid id, UpdateEmployerDto updateEmployerDto, IEmployersRepository repo) =>
        {
            var wasFound = await repo.UpdateEmployer(id, updateEmployerDto.ToEmployer());

            if (wasFound)
            {
                return Results.NoContent();
            }

            return Results.NotFound();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Updates an existing employer")
        .WithDescription("Updates an existing employer by its id.");
        
        group.MapDelete("/{id}", async (Guid id, IEmployersRepository repo) =>
        {
            var wasFound = await repo.DeleteEmployer(id);

            if (wasFound)
            {
                return Results.NoContent();
            }

            return Results.NotFound();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Deletes and employer")
        .WithDescription("Deletes an epmployer form the database by its id.");
    }

};
