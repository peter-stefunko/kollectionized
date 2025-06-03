using System.ComponentModel.DataAnnotations.Schema;

namespace Kollectionized.Api.Models;

public class CardInstance
{
    [Column("id")]
    public Guid Id { get; init; }
    
    [Column("card_id")]
    public Guid CardId { get; init; }
    
    [Column("current_owner")]
    public Guid? CurrentOwner { get; init; }

    [Column("grade")]
    public double? Grade { get; set; }
    
    [Column("grading_company")]
    public string? GradingCompany { get; set; }
    
    [Column("notes")]
    public string Notes { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; init; }
    
    [ForeignKey(nameof(CardId))]
    public PokemonCard? Card { get; init; }
    
    [ForeignKey(nameof(CurrentOwner))]
    public User? Owner { get; init; }
}
