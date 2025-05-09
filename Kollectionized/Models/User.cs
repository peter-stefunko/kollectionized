using System;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public record User(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("bio")] string Bio
);