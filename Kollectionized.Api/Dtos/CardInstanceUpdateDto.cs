namespace Kollectionized.Api.Dtos;

public class CardInstanceUpdateDto
{
    public double? Grade { get; set; } = null;
    public string? GradingCompany { get; set; } = null;
    public string Notes { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}