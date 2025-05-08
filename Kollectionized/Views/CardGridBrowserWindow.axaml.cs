using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardGridBrowserWindow : Window
{
    public CardGridBrowserWindow(string gameKey)
    {
        InitializeComponent();
        DataContext = new CardGridBrowserViewModel(gameKey);
    }
}