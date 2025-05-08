using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardDetailsWindow : Window
{
    public CardDetailsWindow(PokemonCard card)
    {
        InitializeComponent();
        DataContext = new CardDetailsViewModel(card);
    }
}