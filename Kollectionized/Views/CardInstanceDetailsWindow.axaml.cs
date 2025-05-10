using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardInstanceDetailsWindow : Window
{
    public CardInstanceDetailsWindow(PokemonCard card, CardInstance instance)
    {
        InitializeComponent();
        DataContext = new CardInstanceDetailsViewModel(card, instance, new CardImageService());
    }
}