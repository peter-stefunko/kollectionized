namespace Kollectionized.Api.Dtos;

public class AccountUpdateDto
{
    public string Password { get; set; } = string.Empty;
    public string? NewUsername { get; set; }
    public string? Bio { get; set; }
}