using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class UserProfileViewModel : ObservableObject
{
    public Guid ViewedUserId { get; }
    public string ViewedUsername { get; }

    public bool IsCurrentUser => ViewedUserId == AuthService.CurrentUserId;

    [ObservableProperty] private string editableUsername = string.Empty;

    private readonly UserService _userService;

    public UserProfileViewModel(Guid viewedUserId, string username)
    {
        ViewedUserId = viewedUserId;
        ViewedUsername = username;
        EditableUsername = username;
        _userService = new UserService();
    }

    [RelayCommand]
    private async Task SaveUsername()
    {
        if (!IsCurrentUser) return;

        await _userService.ChangeUsername(ViewedUserId, EditableUsername);
    }

    [RelayCommand]
    private async Task DeleteAccount()
    {
        if (!IsCurrentUser) return;

        bool confirmed = await DialogService.ConfirmAsync("Are you sure you want to delete your account?");
        if (confirmed)
        {
            var success = await _userService.DeleteAccount(EditableUsername, "your-password-placeholder");
            if (success)
            {
                AuthService.Logout();
                // TODO: Navigate away or close profile
            }
        }
    }

    [RelayCommand]
    private void Logout()
    {
        AuthService.Logout();
        // TODO: Navigate to AccessWindow if needed
    }
}