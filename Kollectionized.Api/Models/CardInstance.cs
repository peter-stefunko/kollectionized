using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Kollectionized.Api.Models;

public class CardInstance
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("card_id")]
    public Guid CardId { get; set; }
    
    [Column("current_owner")]
    public Guid? CurrentOwner { get; set; }

    [Column("grade")]
    public double? Grade { get; set; }
    
    [Column("grading_company")]
    public string? GradingCompany { get; set; }
    
    [Column("notes")]
    public string Notes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [ForeignKey(nameof(CardId))]
    public PokemonCard? Card { get; set; }
    
    [ForeignKey(nameof(CurrentOwner))]
    public User? Owner { get; set; }
}
