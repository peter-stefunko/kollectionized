using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public class CardInstanceDetailsViewModel(PokemonCard card, CardInstanceDetails instance) : ViewModelBase
{
    public PokemonCard Card { get; } = card;
    public CardInstanceDetails Instance { get; } = instance;
}