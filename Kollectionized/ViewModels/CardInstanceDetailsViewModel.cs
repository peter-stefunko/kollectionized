using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public class CardInstanceDetailsViewModel : ViewModelBase
{
    public PokemonCard Card { get; set; } = new();
    public CardInstanceDetails Instance { get; set; } = new();
}