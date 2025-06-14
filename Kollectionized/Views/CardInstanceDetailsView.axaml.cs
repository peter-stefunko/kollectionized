using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardInstanceDetailsView : UserControl
{
    public CardInstanceDetailsView(PokemonCard card, CardInstance instance)
    {
        InitializeComponent();
        DataContext = new CardInstanceDetailsViewModel(card, instance);
    }
}