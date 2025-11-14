using System;
using JobTracker.Api.Models;
using JobTracker.Api.DTOs;

namespace JobTracker.Api.Maps;

public static class EmployerMaps
{
    public static EmployerDto ToDto(this Employer employer)
    {
        return new EmployerDto
        {
            Id = employer.Id,
            Name = employer.Name,
            CompanyDescription = employer.CompanyDescription
        };
    }

    public static Employer ToEmployer(this CreateEmployerDto createEmployerDto)
    {
        return new Employer
        {
            Name = createEmployerDto.Name,
            CompanyDescription = createEmployerDto.CompanyDescription
        };
    }

    public static Employer ToEmployer(this UpdateEmployerDto updateEmployerDto)
    {
        return new Employer
        {
            Name = updateEmployerDto.Name,
            CompanyDescription = updateEmployerDto.CompanyDescription
        };
    }
}
