using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class UserProfileViewModel : ViewModelBase
{
    private readonly UserService _userService;
    private readonly Action? _onDeleteSuccess;

    public User User { get; }

    public Guid ViewedUserId => User.Id;
    public string ViewedUsername => User.Username;
    public string Bio => User.Bio;
    public DateTime CreatedAt => User.CreatedAt;

    public bool IsCurrentUser => User.Id == AuthService.CurrentUser?.Id;

    [ObservableProperty] private string _editableUsername;

    public CardInstanceGridBrowserViewModel CardGridViewModel { get; }

    public UserProfileViewModel(User user, UserService userService, Action? onDeleteSuccess = null)
    {
        User = user;
        _editableUsername = user.Username;
        _userService = userService;
        _onDeleteSuccess = onDeleteSuccess;

        CardGridViewModel = new CardInstanceGridBrowserViewModel(user);
    }

    [RelayCommand]
    private async Task SaveUsernameAsync()
    {
        if (!IsCurrentUser || string.IsNullOrWhiteSpace(EditableUsername)) return;

        var confirmed = await DialogService.ConfirmAsync(
            $"Are you sure you want to change your username to '{EditableUsername}'?",
            "Confirm Username Change");

        if (!confirmed) return;

        await RunWithLoading(async () =>
        {
            var error = await _userService.ChangeUsername(
                AuthService.CurrentUser?.Username ?? "",
                AuthService.CurrentPassword!,
                EditableUsername);

            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            AuthService.Login(User with { Username = EditableUsername }, AuthService.CurrentPassword!);
        });
    }

    [RelayCommand]
    private async Task DeleteAccountAsync()
    {
        if (!IsCurrentUser) return;

        var confirmed = await DialogService.ConfirmAsync(
            "Are you sure you want to delete your account?",
            "Confirm Account Deletion");

        if (!confirmed) return;

        await RunWithLoading(async () =>
        {
            var error = await _userService.DeleteAccount(ViewedUsername, AuthService.CurrentPassword!);
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            AuthService.Logout();
            _onDeleteSuccess?.Invoke();
        });
    }

    [RelayCommand]
    private static void Logout() => AuthService.Logout();
}