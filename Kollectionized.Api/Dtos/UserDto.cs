namespace Kollectionized.Api.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? LastUsername { get; set; }
    public string? Bio { get; set; } = string.Empty;
}