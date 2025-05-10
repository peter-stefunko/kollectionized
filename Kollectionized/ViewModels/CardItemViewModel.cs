using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public class CardItemViewModel : CardDetailsViewModel
{
    public IRelayCommand ShowDetailsCommand { get; }

    public CardItemViewModel(PokemonCard card, CardImageService imageService)
        : base(card, imageService)
    {
        ShowDetailsCommand = new RelayCommand(OpenDetails);
    }

    private void OpenDetails()
    {
        new CardDetailsWindow(Card).Show();
    }
}
