namespace JobTracker.Api.DTOs;

public record class UpdateEmployerDto
{
    public required string Name { get; set; }
}
