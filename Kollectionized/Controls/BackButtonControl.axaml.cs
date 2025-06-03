using Avalonia.Controls;
using Avalonia.Interactivity;
using Kollectionized.Utils;

namespace Kollectionized.Controls;

public partial class BackButtonControl : UserControl
{
    public BackButtonControl()
    {
        InitializeComponent();
    }

    private void OnClick(object? sender, RoutedEventArgs e)
    {
        AppNavigation.GoBack();
    }
}