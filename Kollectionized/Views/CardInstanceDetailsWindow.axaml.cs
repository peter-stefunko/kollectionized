using System;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardInstanceDetailsWindow : WindowBase
{
    public CardInstanceDetailsWindow(PokemonCard card, CardInstance instance, Action? onDeleted = null)
    {
        InitializeComponent();
        DataContext = new CardInstanceDetailsViewModel(card, instance, onDeleted);
    }
}