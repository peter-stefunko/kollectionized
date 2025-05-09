using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kollectionized.Models;
using Kollectionized;

namespace Kollectionized.Views;

public partial class CardDetailsWindow : Window
{
    public CardDetailsWindow(PokemonCard card)
    {
        InitializeComponent();
        DataContext = ViewModelLocator.CreateCardDetailsViewModel(card);
    }
}