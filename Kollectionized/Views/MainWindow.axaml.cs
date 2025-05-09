using Avalonia.Controls;
using Avalonia.Interactivity;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ViewModelLocator.MainWindow;
    }

    private void BrowseAllCards_Click(object? sender, RoutedEventArgs e)
    {
        new CardGamesWindow().Show();
    }

    private void ViewCardInstance_Click(object? sender, RoutedEventArgs e)
    {
        new CardInstanceDetailsWindow(new PokemonCard(), new CardInstanceDetails()).Show();
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