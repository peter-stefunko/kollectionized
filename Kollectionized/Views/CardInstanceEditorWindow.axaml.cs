using System;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardInstanceEditorWindow : WindowBase
{
    public CardInstanceEditorWindow(CardInstance instance, Action? onDeleted = null)
    {
        InitializeComponent();
        DataContext = new CardInstanceEditorWindowViewModel(instance, Close, onDeleted);
    }
}