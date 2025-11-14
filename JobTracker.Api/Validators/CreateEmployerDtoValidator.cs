namespace JobTracker.Api.Validators;
using FluentValidation;
using JobTracker.Api.DTOs;

public class CreateEmployerDtoValidator : AbstractValidator<CreateEmployerDto>
{
    public CreateEmployerDtoValidator()
    {
        RuleFor(createEmployerDto => createEmployerDto.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(createEmployerDto => createEmployerDto.CompanyDescription)
            .MaximumLength(500);
    }
}