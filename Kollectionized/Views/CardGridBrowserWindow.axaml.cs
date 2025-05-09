using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardGridBrowserWindow : Window
{
    public CardGridBrowserWindow(string gameKey)
    {
        InitializeComponent();
        DataContext = ViewModelLocator.CreateCardGridBrowser(gameKey);
    }

    private void ShowDetails_Clicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is Border { DataContext: CardItemViewModel vm })
        {
            vm.ShowDetailsCommand.Execute(null);
        }
    }
}