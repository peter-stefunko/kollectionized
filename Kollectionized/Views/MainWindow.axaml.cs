using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainWindowViewModel();
        DataContext = _viewModel;
    }

    private async void BrowseAllCards_Click(object? sender, RoutedEventArgs e)
    {
        // new CardBrowserWindow().Show();
    }

    private async void ViewCardInstance_Click(object? sender, RoutedEventArgs e)
    {
        new CardInstanceDetailsWindow().Show();
    }

    private async void ManageCollections_Click(object? sender, RoutedEventArgs e)
    {
        // new DecksAndCollectionsWindow().Show();
    }

    private async void ViewUserProfile_Click(object? sender, RoutedEventArgs e)
    {
        var userId = AuthService.CurrentUserId!.Value;
        var username = AuthService.CurrentUsername!;

        new UserProfileWindow(userId, username).Show();
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
        if (AuthService.CurrentUserId is not { } userId || AuthService.CurrentUsername is null)
            return;

        new UserProfileWindow(userId, AuthService.CurrentUsername).Show();
    }
    private void OpenUserSearch_Click(object? sender, RoutedEventArgs e)
    {
        var sampleId = Guid.NewGuid();
        var sampleName = "searched_user";

        new UserProfileWindow(sampleId, sampleName).Show();
    }
}