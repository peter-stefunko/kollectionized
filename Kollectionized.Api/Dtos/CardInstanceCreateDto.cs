using System;

namespace Kollectionized.Api.Dtos;

public class CardInstanceCreateDto
{
    public Guid CardId { get; set; }
    public double? Grade { get; set; }
    public string? GradingCompany { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}