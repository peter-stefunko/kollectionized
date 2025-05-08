using System;
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
        var viewModel = new MainWindowViewModel();
        DataContext = viewModel;
    }

    private async void BrowseAllCards_Click(object? sender, RoutedEventArgs e)
    {
        new CardGamesWindow().Show();
    }

    private async void ViewCardInstance_Click(object? sender, RoutedEventArgs e)
    {
        new CardInstanceDetailsWindow().Show();
    }

    private async void ManageCollections_Click(object? sender, RoutedEventArgs e)
    {
        // new DecksAndCollectionsWindow().Show();
    }

    private void ShowProfile_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: User user })
        {
            new UserProfileWindow(user.Id, user.Username).Show();
        }
    }


    private async void AccessAccount_Click(object? sender, RoutedEventArgs e)
    {
        await new AccessWindow().ShowDialog(this);
    }

    private async void Logout_Click(object? sender, RoutedEventArgs e)
    {
        MainWindowViewModel.Logout();
    }
    
    private void OpenOwnProfile_Click(object? sender, RoutedEventArgs e)
    {
        var userId = AuthService.CurrentUserId!.Value;
        var username = AuthService.CurrentUsername!;

        new UserProfileWindow(userId, username).Show();
    }
    private void OpenUserSearch_Click(object? sender, RoutedEventArgs e)
    {
        new UserSearchWindow().Show();
    }
}