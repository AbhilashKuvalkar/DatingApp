namespace API.Errors;

public class ApiException(int statusCode, string message, string? description)
{
    public int StatusCode { get; set; } = statusCode;

    public string Message { get; set; } = message;

    public string? Description { get; set; } = description;
}
