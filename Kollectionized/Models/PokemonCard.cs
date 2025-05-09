namespace Kollectionized.Models;

public class PokemonCard
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Set { get; set; } = string.Empty;
    public string? Rarity { get; set; }
    public string Type { get; set; } = string.Empty;
    public int? PokedexNumber { get; set; }
    public string Typings { get; set; } = string.Empty;
    public string Form { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
}