using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public partial class CardItemViewModel : ViewModelBase
{
    private readonly CardImageService _imageService;

    public PokemonCard Card { get; }
    public bool IsLoggedIn => AuthService.IsLoggedIn;

    public IRelayCommand ShowDetailsCommand { get; }

    [ObservableProperty] private Bitmap? _image;

    public CardItemViewModel(PokemonCard card, CardImageService imageService)
    {
        Card = card;
        _imageService = imageService;
        ShowDetailsCommand = new RelayCommand(OpenDetails);
        _ = LoadImageAsync();
    }

    private async Task LoadImageAsync()
    {
        Image = await _imageService.LoadCardImageAsync(Card);
    }

    private void OpenDetails()
    {
        new CardDetailsWindow(Card).Show(); // Ideally inject this later
    }
}