using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kollectionized.Api.Models;

[Table("pokemon_cards")]
public class PokemonCard
{
    [Key]
    public Guid Uuid { get; set; }

    [Column("set")]
    public string Set { get; set; } = string.Empty;
    
    [Column("card_number")]
    public string CardNumber { get; set; } = string.Empty;
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("type")]
    public string Type { get; set; } = string.Empty;
    
    [Column("form", TypeName = "jsonb")]
    public string Forms { get; set; } = string.Empty;
    
    [Column("image_url")]
    public string ImageUrl { get; set; } = string.Empty;
    
    [Column("rarity")]
    public string? Rarity { get; set; }
    
    [Column("pokedex_number")]
    public int? PokedexNumber { get; set; }
    
    [Column("typings", TypeName = "jsonb")]
    public string Typings { get; set; } = string.Empty;
}
