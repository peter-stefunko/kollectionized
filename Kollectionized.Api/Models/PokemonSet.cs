using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kollectionized.Api.Models;

public class PokemonSet
{
    [Key]
    public string Name { get; set; } = string.Empty;
    
    [Column("code")]
    public string? Code { get; set; }
    
    [Column("total_cards")]
    public long? TotalCards { get; set; }
    
    [Column("release_date")]
    public DateTime? ReleaseDate { get; set; }
}
