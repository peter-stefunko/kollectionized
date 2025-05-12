using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Dto;

namespace Kollectionized.ViewModels;

public partial class ManageAccountViewModel(UserProfileViewModel profile, Action? onDeleteSuccess = null)
    : ViewModelBase
{
    private UserProfileViewModel Profile { get; } = profile;

    [ObservableProperty] private string _editableUsername = profile.User.Username;
    [ObservableProperty] private string _editableBio = profile.User.Bio;
    [ObservableProperty] private string _currentPassword = string.Empty;
    [ObservableProperty] private string _newPassword = string.Empty;
    [ObservableProperty] private string _confirmNewPassword = string.Empty;

    [RelayCommand]
    private async Task SaveChangesAsync()
    {
        if (!Profile.IsCurrentUser || string.IsNullOrWhiteSpace(EditableUsername)) return;

        var msgBox = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ButtonDefinitions = ButtonEnum.YesNoCancel,
            ContentTitle = "Confirm username change",
            ContentMessage = "Save user profile changes?",
            Icon = Icon.Question,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        });

        var result = await msgBox.ShowAsync();
        if (result != ButtonResult.Yes)
            return;

        await RunWithLoading(async () =>
        {
            var error = await UserService.UpdateAccount(
                AuthService.CurrentUser!.Username,
                AuthService.CurrentPassword!,
                EditableUsername,
                EditableBio);

            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            /*var updatedUser = AuthService.CurrentUser;
            updatedUser.Username = EditableUsername;
            updatedUser.Bio = EditableBio;*/

            var updatedUser = AuthService.CurrentUser with { Username = EditableUsername, Bio = EditableBio };
            AuthService.Login(updatedUser, AuthService.CurrentPassword!);
            Profile.Refresh(updatedUser);
        });
    }

    [RelayCommand]
    private async Task ChangePasswordAsync()
    {
        if (string.IsNullOrWhiteSpace(CurrentPassword) ||
            string.IsNullOrWhiteSpace(NewPassword) ||
            NewPassword != ConfirmNewPassword)
        {
            ErrorMessage = "Password fields are invalid or do not match.";
            return;
        }

        await RunWithLoading(async () =>
        {
            var error = await UserService.ChangePassword(
                Profile.User.Username,
                CurrentPassword,
                NewPassword);

            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            AuthService.Logout();
        });
    }

    [RelayCommand]
    private async Task DeleteAccountAsync()
    {
        var msgBox = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ButtonDefinitions = ButtonEnum.YesNoCancel,
            ContentTitle = "Confirm",
            ContentMessage = "Are you sure you want to delete your account?",
            Icon = Icon.Warning,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        });

        var confirmed = await msgBox.ShowAsync();
        if (confirmed != ButtonResult.Yes)
            return;

        await RunWithLoading(async () =>
        {
            var error = await UserService.DeleteAccount(AuthService.CurrentUser!.Username, AuthService.CurrentPassword!);
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            AuthService.Logout();
            onDeleteSuccess?.Invoke();
        });
    }
}