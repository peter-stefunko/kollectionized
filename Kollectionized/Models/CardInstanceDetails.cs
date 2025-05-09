using System;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class CardInstanceDetails
{
    [JsonPropertyName("id")]
    public Guid CardId { get; init; }

    [JsonPropertyName("grade")]
    public double? Grade { get; init; }

    [JsonPropertyName("gradingCompany")]
    public string? GradingCompany { get; init; }

    [JsonPropertyName("notes")]
    public string Notes { get; init; } = string.Empty;

    [JsonPropertyName("currentOwner")]
    public string? CurrentOwner { get; init; }
}