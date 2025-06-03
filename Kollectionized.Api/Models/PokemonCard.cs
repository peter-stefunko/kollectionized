using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kollectionized.Api.Models;

[Table("pokemon_cards")]
public class PokemonCard
{
    [Key]
    public Guid Uuid { get; init; }

    [Column("set")]
    public string Set { get; init; } = string.Empty;
    
    [Column("card_number")]
    public string CardNumber { get; init; } = string.Empty;
    
    [Column("name")]
    public string Name { get; init; } = string.Empty;
    
    [Column("type")]
    public string Type { get; init; } = string.Empty;
    
    [Column("form", TypeName = "jsonb")]
    public string Forms { get; init; } = string.Empty;
    
    [Column("image_url")]
    public string ImageUrl { get; init; } = string.Empty;
    
    [Column("rarity")]
    public string? Rarity { get; init; }
    
    [Column("pokedex_number")]
    public int? PokedexNumber { get; init; }
    
    [Column("typings", TypeName = "jsonb")]
    public string? Typings { get; init; }
}