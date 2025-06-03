using Avalonia.Controls;
using Avalonia.Interactivity;
using Kollectionized.Models;
using Kollectionized.Utils;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class UserProfileView : UserControl
{
    private UserProfileViewModel ViewModel => (UserProfileViewModel)DataContext!;

    public UserProfileView(User user)
    {
        InitializeComponent();
        DataContext = new UserProfileViewModel(user);
        _ = ViewModel.InitializeAsync();
    }

    private void OnManageAccountClick(object? sender, RoutedEventArgs e)
    {
        AppNavigation.NavigateTo(new ManageAccountView(ViewModel));
    }

    private void OnCreateDeckClick(object? sender, RoutedEventArgs e)
    {
        AppNavigation.NavigateTo(new DeckCreateView());
    }

    private void OnEditDeckClick(object? sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedDeck is { } deck)
        {
            AppNavigation.NavigateTo(new DeckEditView(deck));
        }
    }
}