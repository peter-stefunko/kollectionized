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
    public PokemonCard Card { get; }

    [ObservableProperty] private Bitmap? _image;

    public IRelayCommand AddInstanceCommand { get; }

    public CardDetailsViewModel(PokemonCard card)
    {
        Card = card;
        AddInstanceCommand = new RelayCommand(OpenAddMenu, () => IsLoggedIn);
        _ = RunWithLoading(LoadImageAsync);
    }

    private async Task LoadImageAsync()
    {
        Image = await CardImageService.LoadCardImageAsync(Card);
    }

    private void OpenAddMenu()
    {
        new AddCardMenuWindow(Card).Show();
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        AddInstanceCommand.NotifyCanExecuteChanged();
    }
}