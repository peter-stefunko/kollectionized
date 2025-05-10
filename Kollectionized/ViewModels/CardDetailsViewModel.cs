using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public partial class CardDetailsViewModel : ViewModelBase
{
    private readonly CardImageService _imageService;

    public PokemonCard Card { get; }

    [ObservableProperty] private Bitmap? _image;

    public bool IsLoggedIn => AuthService.IsLoggedIn;

    public IRelayCommand AddInstanceCommand { get; }

    public CardDetailsViewModel(PokemonCard card, CardImageService imageService)
    {
        Card = card;
        _imageService = imageService;

        AddInstanceCommand = new RelayCommand(OpenAddMenu, () => IsLoggedIn);
        _ = LoadImageAsync();
    }

    private async Task LoadImageAsync()
    {
        Image = await _imageService.LoadCardImageAsync(Card);
    }

    private void OpenAddMenu()
    {
        new AddCardMenuWindow(Card).Show();
    }
}