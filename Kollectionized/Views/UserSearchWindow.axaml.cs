using Avalonia.Controls;
using Avalonia.Interactivity;
using Kollectionized;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class UserSearchWindow : Window
{
    public UserSearchWindow()
    {
        InitializeComponent();
        DataContext = ViewModelLocator.UserSearch;
    }

    private void Username_Clicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is TextBlock { DataContext: UserListItemViewModel vm })
        {
            vm.ShowProfileCommand.Execute(null);
        }
    }
}