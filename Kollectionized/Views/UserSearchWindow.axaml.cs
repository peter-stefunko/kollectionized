using Avalonia.Controls;
using Avalonia.Interactivity;
using Kollectionized.Models;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class UserSearchWindow : Window
{
    public UserSearchWindow()
    {
        InitializeComponent();
        DataContext = new UserSearchViewModel();
    }
    
    private void Username_Clicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is TextBlock tb && tb.DataContext is UserListItemViewModel vm)
        {
            vm.ShowProfileCommand.Execute(null);
        }
    }
}