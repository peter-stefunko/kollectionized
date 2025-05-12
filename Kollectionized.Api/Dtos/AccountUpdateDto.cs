namespace Kollectionized.Api.Dtos;

public class AccountUpdateDto
{
    public string Password { get; set; } = string.Empty;
    public string? NewUsername { get; set; } = null;
    public string Bio { get; set; } = string.Empty;
}