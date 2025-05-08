using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Kollectionized.Models;
using Kollectionized.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Kollectionized.ViewModels;

public partial class CardItemViewModel : ObservableObject
{
    public PokemonCard Card { get; }
    public bool IsLoggedIn => AuthService.IsLoggedIn;
    
    [ObservableProperty]
    private Bitmap? _image;

    public CardItemViewModel(PokemonCard card)
    {
        Card = card;
        _ = LoadImageAsync();
    }

    private async Task LoadImageAsync()
    {
        Image = await Card.GetImageAsync();
    }
}