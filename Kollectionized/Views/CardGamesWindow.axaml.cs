using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardGamesWindow : Window
{
    public CardGamesWindow()
    {
        InitializeComponent();
        DataContext = ViewModelLocator.CardGames;
    }

    private void GameOption_Clicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is Control { DataContext: CardGameOptionViewModel vm })
        {
            vm.OpenCommand.Execute(null);
        }
    }
}