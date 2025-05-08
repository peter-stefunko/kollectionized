using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Kollectionized.Models;
using Kollectionized.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public partial class CardItemViewModel : ObservableObject
{
    public PokemonCard Card { get; }
    public IRelayCommand ShowDetailsCommand { get; }
    public bool IsLoggedIn => AuthService.IsLoggedIn;
    
    [ObservableProperty]
    private Bitmap? _image;

    public CardItemViewModel(PokemonCard card)
    {
        Card = card;
        _ = LoadImageAsync();
        ShowDetailsCommand = new RelayCommand(OpenDetails);
    }

    private async Task LoadImageAsync()
    {
        Image = await Card.GetImageAsync();
    }
    
    private void OpenDetails()
    {
        new CardDetailsWindow(Card).Show();
    }
    
}