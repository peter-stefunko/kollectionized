using Avalonia.Controls;
using Avalonia.Input;
using Kollectionized.ViewModels;

namespace Kollectionized.Views;

public partial class UserSearchView : UserControl
{
    public UserSearchView()
    {
        InitializeComponent();
        DataContext = new UserSearchViewModel();
    }

    private void OnUserClicked(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border { DataContext: UserItemViewModel vm })
        {
            vm.ShowProfileCommand.Execute(null);
        }
    }
}