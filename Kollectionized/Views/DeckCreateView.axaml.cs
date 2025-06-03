using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class DeckCreateView : UserControl
{
    public DeckCreateView()
    {
        InitializeComponent();
        DataContext = new DeckCreateViewModel();
    }
}