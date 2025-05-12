using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardDetailsWindow : WindowBase
{
    public CardDetailsWindow(PokemonCard card)
    {
        InitializeComponent();
        DataContext = new CardDetailsViewModel(card);
    }
}