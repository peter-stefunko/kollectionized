using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public class CardDetailsViewModel(PokemonCard card) : ViewModelBase
{
    public PokemonCard Card { get; } = card;
}