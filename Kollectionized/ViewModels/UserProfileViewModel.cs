using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public partial class UserProfileViewModel : ViewModelBase
{
    public User User { get; private set; }
    public bool IsCurrentUser => User.Id == AuthService.CurrentUser?.Id;
    public CardInstanceGridBrowserViewModel CardGridViewModel { get; }

    private readonly Action? _onUserNotFound;

    public UserProfileViewModel(User user, Action? onUserNotFound = null)
    {
        User = user;
        _onUserNotFound = onUserNotFound;
        CardGridViewModel = new CardInstanceGridBrowserViewModel(user);
    }

    public void Refresh(User updatedUser)
    {
        User = updatedUser;
        OnPropertyChanged(nameof(User));
        OnPropertyChanged(nameof(IsCurrentUser));
    }

    /*[RelayCommand]
    private void RefreshProfile()
    {
        if (AuthService.CurrentUser is not null)
        {
            Refresh(AuthService.CurrentUser);
        }
    }*/

    [RelayCommand]
    private async Task RefreshAllAsync()
    {
        await RunWithLoading(async () =>
        {
            var latest = await UserService.GetUserById(User.Id);
            if (latest == null)
            {
                await ShowUserNotFoundAndCloseWindow();
                return;
            }

            Refresh(latest);
            CardGridViewModel.ForceRefresh();
        });
    }

    private async Task ShowUserNotFoundAndCloseWindow()
    {
        var box = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ContentTitle = "User Not Found",
            ContentMessage = "This user no longer exists.",
            ButtonDefinitions = ButtonEnum.Ok,
            Icon = Icon.Error
        });

        await box.ShowAsync();
        _onUserNotFound?.Invoke();
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(IsCurrentUser));
        CardGridViewModel.NotifySessionChanged();
    }
}