using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public class CardDetailsViewModel : ViewModelBase
{
    public PokemonCard Card { get; set; } = new();
}