using System;
using System.Text.Json.Serialization;

namespace Kollectionized.Models;

public class PokemonSet
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    
    [JsonPropertyName("totalCards")]
    public long? TotalCards { get; set; }
    
    [JsonPropertyName("releaseDate")]
    public DateTime? ReleaseDate { get; set; }
}
