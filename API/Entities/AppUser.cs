namespace API.Entities;

public class AppUser
{
    public required Guid Id { get; set; } = Guid.CreateVersion7();

    public required string DisplayName { get; set; }

    public required string Email { get; set; }
}