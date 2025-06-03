using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class PokemonDeck
{
    [JsonPropertyName("id")] public Guid Id { get; init; }

    [JsonPropertyName("userId")] public Guid? UserId { get; init; }

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;

    [JsonPropertyName("isPublic")] public bool IsPublic { get; init; }

    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; init; }

    [JsonPropertyName("user")] public User? User { get; init; }

    [JsonPropertyName("cardInstances")] public List<CardInstance> CardInstances { get; init; } = [];
}