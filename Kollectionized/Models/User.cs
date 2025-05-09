using System;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class User
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("username")]
    public string Username { get; init; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("bio")]
    public string Bio { get; init; } = string.Empty;
}