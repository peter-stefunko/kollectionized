using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.Utils;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public partial class CardItemViewModel : ViewModelBase
{
    public PokemonCard Card { get; }

    [ObservableProperty]
    private Bitmap? _image;

    public IRelayCommand ShowDetailsCommand { get; }

    public CardItemViewModel(PokemonCard card)
    {
        Card = card;
        ShowDetailsCommand = new RelayCommand(ShowDetails);
        _ = LoadImageAsync();
    }

    private async Task LoadImageAsync()
    {
        Image = await CardImageService.LoadCardImageAsync(Card);
    }

    protected virtual void ShowDetails()
    {
        AppNavigation.NavigateTo(new CardDetailsView(Card));
    }
}