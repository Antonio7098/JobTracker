namespace JobTracker.Api.DTOs;

public record class UpdateEmployerDto
{
    public required string Name { get; set; }
    public string? CompanyDescription { get; set; }
    
}
