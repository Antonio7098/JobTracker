using System;
using Microsoft.AspNetCore.Components.Web;

namespace JobTracker.Api.Models;

public class JobVacancy
{
    public int Id { get; set; }
    public required string PageTitle { get; set; }
    public string? Description { get; set; }
    public DateOnly? Deadline { get; set; }
    public int? EmployerId { get; set; }
    public Employer? Employer { get; set; }

}
