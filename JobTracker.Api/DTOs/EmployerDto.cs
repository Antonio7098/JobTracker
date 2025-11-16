namespace JobTracker.Api.DTOs;

public record class EmployerDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? CompanyDescription { get; set; }
}
