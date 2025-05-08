using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Kollectionized.Api.Models;

[Table("pokemon_decks")]
public class PokemonDecks
{
    [Key]
    public Guid Id { get; init; }

    [Column("user_id")]
    public Guid? UserId { get; init; }

    [Column("name")]
    public string Name { get; init; } = string.Empty;

    [Column("description")]
    public string Description { get; init; } = string.Empty;

    [Column("is_public")]
    public bool IsPublic { get; init; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}