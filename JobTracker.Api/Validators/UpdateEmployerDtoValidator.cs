using FluentValidation;
using JobTracker.Api.DTOs;

namespace JobTracker.Api.Validators;

public class UpdateEmployerDtoValidator : AbstractValidator<UpdateEmployerDto>
{
    public UpdateEmployerDtoValidator()
    {
        RuleFor(updateEmployerDto => updateEmployerDto.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(updateEmployerDto => updateEmployerDto.CompanyDescription)
            .MaximumLength(500);
    }
}
