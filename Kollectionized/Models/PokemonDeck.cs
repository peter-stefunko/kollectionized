using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class PokemonDeck
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("userId")]
    public Guid? UserId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("isPublic")]
    public bool IsPublic { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("user")]
    public User? User { get; set; } = null;

    [JsonPropertyName("cardInstances")]
    public List<CardInstance> CardInstances { get; set; } = new();
}