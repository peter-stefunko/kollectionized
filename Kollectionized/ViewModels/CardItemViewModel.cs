using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public class CardItemViewModel : CardDetailsViewModel
{
    public IRelayCommand ShowDetailsCommand { get; }

    public CardItemViewModel(PokemonCard card)
        : base(card)
    {
        ShowDetailsCommand = new RelayCommand(OpenDetails);
    }

    private void OpenDetails()
    {
        new CardDetailsWindow(Card).Show();
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        AddInstanceCommand.NotifyCanExecuteChanged();
    }
}