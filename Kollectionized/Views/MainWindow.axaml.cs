using Avalonia.Interactivity;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class MainWindow : WindowBase
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void BrowseAllCards_Click(object? sender, RoutedEventArgs e)
    {
        new CardGamesWindow().Show();
    }

    private async void AccessAccount_Click(object? sender, RoutedEventArgs e)
    {
        await new AccessWindow().ShowDialog(this);
    }

    private void Logout_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
            vm.LogoutCommand.Execute(null);
    }

    private void OpenOwnProfile_Click(object? sender, RoutedEventArgs e)
    {
        var user = AuthService.CurrentUser;
        if (user != null)
        {
            new UserProfileWindow(user).Show();
        }
    }

    private void OpenUserSearch_Click(object? sender, RoutedEventArgs e)
    {
        new UserSearchWindow().Show();
    }
}