using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kollectionized;

namespace Kollectionized.Views;

public partial class AccessWindow : Window
{
    public AccessWindow()
    {
        InitializeComponent();
        DataContext = ViewModelLocator.CreateAccessWindowViewModel(Close);
    }
}