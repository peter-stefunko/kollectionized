using Kollectionized.Models;
using Kollectionized.Utils;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public class CardInstanceItemViewModel(PokemonCard card, CardInstance instance) : CardItemViewModel(card)
{
    public CardInstance Instance { get; } = instance;

    protected override void ShowDetails()
    {
        AppNavigation.NavigateTo(new CardInstanceDetailsView(Card, Instance));
    }
}