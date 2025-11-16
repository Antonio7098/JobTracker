using System;

namespace JobTracker.Api.Models;

public class JobVacancy
{
    public Guid Id { get; set; }
    public required string PageTitle { get; set; }
    public string? Description { get; set; }
    public DateOnly? Deadline { get; set; }
    public Guid? EmployerId { get; set; }
    public Employer? Employer { get; set; }

}
