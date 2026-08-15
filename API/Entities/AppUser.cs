namespace API.Entities;

public class AppUser
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public required string DisplayName { get; set; }

    public required string Email { get; set; }

    public string? ImageUrl { get; set; }

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }

    // Navigation Property

    public Member Member { get; set; } = null!;
    
}