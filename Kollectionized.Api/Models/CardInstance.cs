namespace Kollectionized.Api.Models;

public class CardInstance
{
    public Guid Id { get; set; }

    public Guid CardId { get; set; }
    public Guid? CurrentOwner { get; set; }

    public double? Grade { get; set; }
    public string GradingCompany { get; set; }
    public string Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public PokemonCard? Card { get; set; }
}
