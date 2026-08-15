using System;

namespace API.DTOs;

public class SeedUserDto
{
    public required Guid Id { get; set; }

    public string? Email { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? ImageUrl { get; set; }

    public string? DisplayName { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    public required string? Gender { get; set; }

    public string? Description { get; set; }

    public required string City { get; set; }

    public required string Country { get; set; }

}
