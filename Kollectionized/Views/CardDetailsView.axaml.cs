using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Controls;

public partial class CardDetailsView : UserControl
{
    public CardDetailsView(PokemonCard card)
    {
        InitializeComponent();
        DataContext = new CardDetailsViewModel(card);
    }
}