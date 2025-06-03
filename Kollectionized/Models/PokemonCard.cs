using System;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class PokemonCard
{
    [JsonPropertyName("uuid")] public Guid Uuid { get; init; }
    
    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;
    
    [JsonPropertyName("imageUrl")] public string ImageUrl { get; init; } = string.Empty;
    
    [JsonPropertyName("set")] public string Set { get; init; } = string.Empty;
    
    [JsonPropertyName("rarity")] public string? Rarity { get; init; }
    
    [JsonPropertyName("type")] public string? Type { get; init; }
    
    [JsonPropertyName("pokedexNumber")] public int? PokedexNumber { get; init; }
    
    [JsonPropertyName("typings")] public string? Typings { get; init; }
    
    [JsonPropertyName("form")] public string? Form { get; init; }
    
    [JsonPropertyName("cardNumber")] public string CardNumber { get; init; } = string.Empty;
}