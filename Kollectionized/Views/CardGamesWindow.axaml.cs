using Avalonia.Controls;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardGamesWindow : Window
{
    public CardGamesWindow()
    {
        InitializeComponent();
        DataContext = new CardGamesViewModel();
    }

    private void GameOption_Clicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is Control { DataContext: CardGameOption option })
        {
            option.OpenCommand.Execute(null);
        }
    }

}