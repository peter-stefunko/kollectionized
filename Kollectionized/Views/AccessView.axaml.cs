using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class AccessView : UserControl
{
    public AccessView()
    {
        InitializeComponent();
        DataContext = new AccessViewModel();
    }
}