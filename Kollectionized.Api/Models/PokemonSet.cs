using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kollectionized.Api.Models;

public class PokemonSet
{
    [Key]
    public string Name { get; init; } = string.Empty;
    
    [Column("code")]
    public string? Code { get; init; }
    
    [Column("total_cards")]
    public long? TotalCards { get; init; }
    
    [Column("release_date")]
    public DateTime? ReleaseDate { get; init; }
}
