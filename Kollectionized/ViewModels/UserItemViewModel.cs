using CommunityToolkit.Mvvm.Input;
using Kollectionized.Controls;
using Kollectionized.Models;
using Kollectionized.Utils;
using Kollectionized.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public class UserListItemViewModel : ViewModelBase
{
    private User User { get; }
    
    public string Username => User.Username;

    public IRelayCommand ShowProfileCommand { get; }

    public UserListItemViewModel(User user)
    {
        User = user;
        ShowProfileCommand = new RelayCommand(OpenProfile);
    }

    private async void OpenProfile()
    {
        var freshUser = await UserService.GetUserById(User.Id);
        if (freshUser is null)
        {
            await MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
            {
                ContentTitle = "Not Found",
                ContentMessage = "User no longer exists.",
                ButtonDefinitions = ButtonEnum.Ok,
                Icon = Icon.Error
            }).ShowAsync();
            return;
        }
        
        AppNavigation.NavigateTo(new UserProfileView(freshUser));
    }
}