using Avalonia.Controls;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardGridBrowserWindow : WindowBase
{
    public CardGridBrowserWindow(string gameKey)
    {
        InitializeComponent();
        DataContext = new CardGridBrowserViewModel(gameKey);
    }

    private void ShowDetails_Clicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is Border { DataContext: CardItemViewModel vm })
        {
            vm.ShowDetailsCommand.Execute(null);
        }
    }
}