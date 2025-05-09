using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardInstanceDetailsWindow : Window
{
    public CardInstanceDetailsWindow(PokemonCard card, CardInstanceDetails instance)
    {
        InitializeComponent();
        DataContext = new CardInstanceDetailsViewModel(card, instance);
    }
}