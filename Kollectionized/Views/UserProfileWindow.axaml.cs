using Avalonia.Controls;
using Kollectionized.ViewModels;
using Kollectionized.Views;

namespace Kollectionized.Views;

public partial class UserProfileWindow : Window
{
    public UserProfileWindow(UserProfileViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void ManageAccount_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is UserProfileViewModel vm)
        {
            new ManageAccountWindow(vm).Show();
        }
    }
}