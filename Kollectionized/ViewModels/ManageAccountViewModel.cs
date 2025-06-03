using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using Kollectionized.State;
using Kollectionized.Utils;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public partial class ManageAccountViewModel : ViewModelBase
{
    private UserProfileViewModel Profile { get; }

    [ObservableProperty] private string _editableUsername;
    [ObservableProperty] private string _editableBio;
    [ObservableProperty] private string _currentPassword = string.Empty;
    [ObservableProperty] private string _newPassword = string.Empty;
    [ObservableProperty] private string _confirmNewPassword = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    public ManageAccountViewModel(UserProfileViewModel profile)
    {
        Profile = profile;
        EditableUsername = profile.User.Username;
        EditableBio = profile.User.Bio;
    }

    [RelayCommand]
    private async Task SaveChangesAsync()
    {
        if (!Profile.IsCurrentUser || string.IsNullOrWhiteSpace(EditableUsername)) return;

        var result = await MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ButtonDefinitions = ButtonEnum.YesNoCancel,
            ContentTitle = "Confirm username change",
            ContentMessage = "Save user profile changes?",
            Icon = Icon.Question,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        }).ShowAsync();

        if (result != ButtonResult.Yes)
            return;

        await RunWithLoading(async () =>
        {
            var error = await UserService.UpdateAccount(
                CurrentUserState.User!.Username,
                CurrentUserState.Password!,
                EditableUsername,
                EditableBio);

            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            var updatedUser = CurrentUserState.User with { Username = EditableUsername, Bio = EditableBio };
            await AuthService.TryLogin(updatedUser.Username, CurrentUserState.Password!);
            Profile.RefreshAllCommand.Execute(null);
            AppNavigation.GoBack();
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
        var confirmed = await MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ButtonDefinitions = ButtonEnum.YesNoCancel,
            ContentTitle = "Confirm",
            ContentMessage = "Are you sure you want to delete your account?",
            Icon = Icon.Warning
        }).ShowAsync();

        if (confirmed != ButtonResult.Yes)
            return;

        await RunWithLoading(async () =>
        {
            var error = await UserService.DeleteAccount(CurrentUserState.User!.Username, CurrentUserState.Password!);
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            AuthService.Logout();
            AppNavigation.GoBack();
        });
    }
}
