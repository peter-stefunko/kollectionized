namespace Kollectionized.Models;

public record PokemonCard
{
    public string Name { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public string Set { get; init; } = string.Empty;
    public string? Rarity { get; init; }
    public string Type { get; init; } = string.Empty;
    public int? PokedexNumber { get; init; }
    public string Typings { get; init; } = string.Empty;
    public string Form { get; init; } = string.Empty;
    public string CardNumber { get; init; } = string.Empty;
}