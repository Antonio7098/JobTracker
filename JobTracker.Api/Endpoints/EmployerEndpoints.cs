using System;
using JobTracker.Api.Models;
using JobTracker.Api.DTOs;
using JobTracker.Api.Services;
using JobTracker.Api.Maps;
using FluentValidation;
using JobTracker.Api.Validators;
using Microsoft.AspNetCore.Mvc;


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
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
        .WithSummary("Gets a list of all employers")
        .WithDescription("Retrieves a complete list of all employers currently stored in the database.");

        group.MapGet("/{id}", async (Guid id, IEmployersRepository repo) =>
        {

            Employer? employer = await repo.GetEmployerById(id);

            if (employer is null)
            {
                return Results.Problem(
                    title: "Employer not found",
                    detail: $"The employer with ID {id} was not found.",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            return Results.Ok(employer.ToDto());

        })
        .Produces<EmployerDto>()
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
        .WithSummary("Gets an employer.")
        .WithDescription("Retrieves an employer from the database by its ID. Returns 404 if the employer is not found.");

        group.MapPost("/", async (CreateEmployerDto createEmployerDto, IEmployersRepository repo, IValidator<CreateEmployerDto> createEmployerDtoValidator) =>
        {
            var validationResult = await createEmployerDtoValidator.ValidateAsync(createEmployerDto);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(
                    validationResult.ToDictionary(),
                    statusCode: StatusCodes.Status422UnprocessableEntity
                );
            }

            var employer = createEmployerDto.ToEmployer();
            await repo.CreateEmployer(employer);

            return Results.Created($"/employers/{employer.Id}", employer.ToDto());
        })
        .Produces<EmployerDto>(StatusCodes.Status201Created)
        .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
        .WithSummary("Adds a new employer")
        .WithDescription("Creates a new employer in the database. Returns 422 if validation fails (e.g., name is empty or exceeds max length).");

        group.MapPut("/{id}", async (Guid id, UpdateEmployerDto updateEmployerDto, IEmployersRepository repo, IValidator<UpdateEmployerDto> updateEmployerDtoValidator) =>
        {
            var validationResult = await updateEmployerDtoValidator.ValidateAsync(updateEmployerDto);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(
                    validationResult.ToDictionary(),
                    statusCode: StatusCodes.Status422UnprocessableEntity
                );
            }

            var wasFound = await repo.UpdateEmployer(id, updateEmployerDto.ToEmployer());

            if (!wasFound)
            {
                return Results.Problem(
                    title: "Employer not found",
                    detail: $"The employer with ID {id} was not found.",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity)
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
        .WithSummary("Updates an existing employer")
        .WithDescription("Updates an existing employer by its ID. Returns 404 if the employer is not found, or 422 if validation fails.");
        
        group.MapDelete("/{id}", async (Guid id, IEmployersRepository repo) =>
        {
            var wasFound = await repo.DeleteEmployer(id);

            if (!wasFound)
            {
                return Results.Problem(
                    title: "Employer not found",
                    detail: $"The employer with ID {id} was not found.",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
        .WithSummary("Deletes an employer")
        .WithDescription("Deletes an employer from the database by its ID. Returns 404 if the employer is not found.");
        
        
    }

};
