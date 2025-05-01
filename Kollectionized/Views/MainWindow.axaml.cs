using Avalonia.Controls;
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

    private async void BrowseAllCards_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // new CardBrowserWindow().Show();
    }

    private async void ViewCardInstance_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new CardInstanceDetailsWindow().Show();
    }

    private async void ManageCollections_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // new DecksAndCollectionsWindow().Show();
    }

    private async void ViewUserProfile_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // new UserProfileWindow().Show();
    }

    private async void AccessAccount_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        await new AccessWindow().ShowDialog(this);
        _viewModel.Refresh();
    }

    private async void Logout_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _viewModel.Logout();
    }
}