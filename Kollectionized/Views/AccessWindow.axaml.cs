using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class AccessWindow : Window
{
    public AccessWindow()
    {
        InitializeComponent();
        DataContext = new AccessWindowViewModel(Close);
    }
}