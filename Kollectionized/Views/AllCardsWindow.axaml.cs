using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class AllCardsWindow : WindowBase
{
    public AllCardsWindow(string gameKey)
    {
        InitializeComponent();
        DataContext = new CardGridBrowserViewModel(gameKey);
    }
}