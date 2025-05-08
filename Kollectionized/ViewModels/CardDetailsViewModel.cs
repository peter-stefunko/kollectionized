using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public partial class CardDetailsViewModel : ViewModelBase
{
    public PokemonCard Card { get; }
    
    [ObservableProperty]
    private Bitmap? _image;
    
    public CardDetailsViewModel(PokemonCard card)
    {
        Card = card;
        _ = LoadImageAsync();
    }
    
    private async Task LoadImageAsync()
    {
        Image = await Card.GetImageAsync();
    }
}