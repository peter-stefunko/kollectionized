using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class CardDetailsViewModel : ViewModelBase
{
    private readonly CardImageService _imageService;

    public PokemonCard Card { get; }

    [ObservableProperty] private Bitmap? _image;

    public CardDetailsViewModel(PokemonCard card, CardImageService imageService)
    {
        Card = card;
        _imageService = imageService;
        _ = LoadImageAsync();
    }

    private async Task LoadImageAsync()
    {
        Image = await _imageService.LoadCardImageAsync(Card);
    }
}