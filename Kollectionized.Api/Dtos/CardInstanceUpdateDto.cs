namespace Kollectionized.Api.Dtos;

public class CardInstanceUpdateDto
{
    public double? Grade { get; set; }
    public string? GradingCompany { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}