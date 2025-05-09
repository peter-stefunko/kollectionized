using Avalonia.Controls;

namespace Kollectionized.Views;

public partial class AccessWindow : Window
{
    public AccessWindow()
    {
        InitializeComponent();
        DataContext = ViewModelLocator.CreateAccessWindowViewModel(Close);
    }
}