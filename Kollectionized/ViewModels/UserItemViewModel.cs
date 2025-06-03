using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Utils;
using Kollectionized.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public class UserItemViewModel : ViewModelBase
{
    public User User { get; }

    public IRelayCommand ShowProfileCommand { get; }

    public UserItemViewModel(User user)
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