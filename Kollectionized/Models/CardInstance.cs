using System;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class CardInstance
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    
    [JsonPropertyName("cardId")] public Guid CardId { get; init; }
    
    [JsonPropertyName("currentOwner")] public Guid? CurrentOwner { get; init; }

    [JsonPropertyName("grade")] public double? Grade { get; init; }

    [JsonPropertyName("gradingCompany")] public string? GradingCompany { get; init; }

    [JsonPropertyName("notes")] public string Notes { get; init; } = string.Empty;
    
    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; init; }
    
    [JsonPropertyName("card")] public PokemonCard? Card { get; set; } = null;

    [JsonPropertyName("owner")] public User? Owner { get; set; } = null;
}