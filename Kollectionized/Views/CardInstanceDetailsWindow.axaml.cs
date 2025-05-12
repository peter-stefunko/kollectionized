using System;
using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.Services;
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