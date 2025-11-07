using System;

namespace JobTracker.Api.Models;

public class Employer
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
