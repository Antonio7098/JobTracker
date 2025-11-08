namespace JobTracker.Api.DTOs;

public record class CreateEmployerDto
{
    public required string Name { get; set; }
}
