using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Kollectionized.Views;

public partial class AllCardsWindow : Window
{
    public AllCardsWindow(string gameKey)
    {
        InitializeComponent();
        DataContext = ViewModelLocator.CreateCardGridBrowser(gameKey);
    }
}