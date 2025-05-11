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

public partial class ManageAccountViewModel : ViewModelBase
{
    private readonly UserService _userService;
    private readonly Action? _onDeleteSuccess;

    public UserProfileViewModel Profile { get; }

    [ObservableProperty] private string _editableUsername;
    [ObservableProperty] private string _editableBio;
    [ObservableProperty] private string _currentPassword = string.Empty;
    [ObservableProperty] private string _newPassword = string.Empty;
    [ObservableProperty] private string _confirmNewPassword = string.Empty;

    public ManageAccountViewModel(UserProfileViewModel profile, UserService userService, Action? onDeleteSuccess = null)
    {
        Profile = profile;
        _userService = userService;
        _onDeleteSuccess = onDeleteSuccess;

        _editableUsername = profile.User.Username;
        _editableBio = profile.User.Bio;
    }

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
            var error = await _userService.UpdateAccount(
                AuthService.CurrentUser!.Username,
                AuthService.CurrentPassword!,
                EditableUsername,
                EditableBio);

            if (error != null)
            {
                ErrorMessage = error;
                return;
            }
            
            AuthService.Login(Profile.User with { Username = EditableUsername, Bio = EditableBio }, AuthService.CurrentPassword!);
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
            var error = await _userService.ChangePassword(
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
            var error = await _userService.DeleteAccount(AuthService.CurrentUser!.Username, AuthService.CurrentPassword!);
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            AuthService.Logout();
            _onDeleteSuccess?.Invoke();
        });
    }
}