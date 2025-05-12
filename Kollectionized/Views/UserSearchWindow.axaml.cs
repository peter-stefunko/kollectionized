using Avalonia.Controls;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class UserSearchWindow : WindowBase
{
    public UserSearchWindow()
    {
        InitializeComponent();
        DataContext = new UserSearchViewModel();
    }

    private void Username_Clicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is TextBlock { DataContext: UserListItemViewModel vm })
        {
            vm.ShowProfileCommand.Execute(null);
        }
    }
}