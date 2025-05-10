namespace Kollectionized.Api.Controllers;

public class CardInstanceAddDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public Guid CardId { get; set; }
    public double? Grade { get; set; }
    public string? GradingCompany { get; set; }
    public string Notes { get; set; } = string.Empty;
}