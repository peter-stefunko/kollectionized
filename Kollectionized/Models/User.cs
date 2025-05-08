using System;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class User
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
}