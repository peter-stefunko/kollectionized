using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class UserProfileWindow : WindowBase
{
    public UserProfileWindow(User user)
    {
        InitializeComponent();
        DataContext = new UserProfileViewModel(user, Close);
    }

    private void ManageAccount_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is UserProfileViewModel vm)
        {
            new ManageAccountWindow(vm).Show();
        }
    }
    
    private void EditDeck_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is UserProfileViewModel vm && vm.SelectedDeck is { } deck)
        {
            new DeckEditorWindow(deck, onClose: () => vm.SwitchToDecksCommand.Execute(null),
                    onDelete: async () => await vm.DeleteSelectedDeckCommand.ExecuteAsync(null))
                .Show();
        }
    }
    
    private void CreateDeck_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is UserProfileViewModel vm)
        {
            new CreateDeckWindow(vm.User.Username, () => vm.SwitchToDecksCommand.Execute(null)).Show();
        }
    }
}