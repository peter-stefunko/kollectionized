using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class CardDetailsView : UserControl
{
    public CardDetailsView()
    {
        InitializeComponent();
        /*DataContext = new CardDetailsViewModel();*/
    }
}