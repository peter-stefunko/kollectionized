using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class AllCardsView : UserControl
{
    public AllCardsView()
    {
        InitializeComponent();
        DataContext = new CardBrowserViewModel();
    }
}