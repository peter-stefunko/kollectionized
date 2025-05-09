using Avalonia.Controls;
using Kollectionized.Models;

namespace Kollectionized.Views;

public partial class CardDetailsWindow : Window
{
    public CardDetailsWindow(PokemonCard card)
    {
        InitializeComponent();
        DataContext = ViewModelLocator.CreateCardDetailsViewModel(card);
    }
}