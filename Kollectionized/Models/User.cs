using System;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class User
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("username")]
    public string Username { get; init; } = string.Empty;
}