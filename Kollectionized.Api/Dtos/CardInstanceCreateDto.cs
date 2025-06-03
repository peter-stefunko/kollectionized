namespace Kollectionized.Api.Dtos;

public class CardInstanceCreateDto
{
    public Guid CardId { get; set; }
    public double? Grade { get; set; } = null;
    public string? GradingCompany { get; set; } = null;
    public string Notes { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}