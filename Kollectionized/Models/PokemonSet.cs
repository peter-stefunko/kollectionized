using System;

namespace Kollectionized.Models;

public class PokemonSet
{
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; }
    public long? TotalCards { get; set; }
    public DateTime? ReleaseDate { get; set; }
}
