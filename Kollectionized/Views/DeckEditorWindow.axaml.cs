using System;
using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class DeckEditorWindow : Window
{
    public DeckEditorWindow(PokemonDeck deck, Action onClose, Action onDelete)
    {
        InitializeComponent();
        DataContext = new DeckEditorWindowViewModel(deck, onClose, onDelete);
    }
}