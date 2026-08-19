namespace API.DTOs;

public class UserDTO
{
    public required Guid Id { get; set; }

    public required string Email { get; set; }

    public required string DisplayName { get; set; }

    public string? ImageUrl { get; set; }
}

public class UserAuthDTO : UserDTO
{

    public required string Token { get; set; }
}