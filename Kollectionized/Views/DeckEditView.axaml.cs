using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class DeckEditView : UserControl
{
    public DeckEditView(PokemonDeck deck)
    {
        InitializeComponent();
        DataContext = new DeckEditViewModel(deck);
    }
}