using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardGamesWindow : WindowBase
{
    public CardGamesWindow()
    {
        InitializeComponent();
        DataContext = new CardGamesViewModel();
    }

    private void GameOption_Clicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is Control { DataContext: CardGameOptionViewModel vm })
        {
            vm.OpenCommand.Execute(null);
        }
    }
}