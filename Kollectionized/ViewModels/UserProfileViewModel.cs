using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Kollectionized.Services;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kollectionized.ViewModels;

public partial class UserProfileViewModel : ViewModelBase
{
    private Guid ViewedUserId { get; }
    public string ViewedUsername { get; set; }

    public bool IsCurrentUser => ViewedUserId == AuthService.CurrentUserId;

    [ObservableProperty] private string _editableUsername = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string? _errorMessage = string.Empty;
    
    private readonly UserService _userService;
    private readonly Action? _onDeleteSuccess;

    public UserProfileViewModel(Guid viewedUserId, string username, Action? onDeleteSuccess = null)
    {
        ViewedUserId = viewedUserId;
        ViewedUsername = username;
        EditableUsername = username;
        _userService = new UserService();
        _onDeleteSuccess = onDeleteSuccess;
    }

    [RelayCommand]
    private async Task SaveUsername()
    {
        if (!IsCurrentUser)
            return;
        
        var confirmed = await DialogService.ConfirmAsync(
            $"Are you sure you want to change your username to '{EditableUsername}'?",
            "Confirm Username Change");

        if (!confirmed)
            return;
        
        var error = await _userService.ChangeUsername(
            AuthService.CurrentUsername!,
            Password,
            EditableUsername
        );
        
        if (error != null)
        {
            ErrorMessage = error;
            return;
        }

        ErrorMessage = string.Empty;
        ViewedUsername = EditableUsername;
        AuthService.Login(AuthService.CurrentUserId!.Value, EditableUsername);
    }


    [RelayCommand]
    private async Task DeleteAccount()
    {
        if (!IsCurrentUser) return;
        
        var confirmed = await DialogService.ConfirmAsync("Are you sure you want to delete your account?","Confirm Account Deletion");
        if (!confirmed)
            return;

        var error = await _userService.DeleteAccount(ViewedUsername, Password);
        if (error != null)
        {
            ErrorMessage = error;
            return;
        }
        
        ErrorMessage = string.Empty;
        AuthService.Logout();
        _onDeleteSuccess?.Invoke();
    }

    [RelayCommand]
    private static void Logout()
    {
        AuthService.Logout();
    }
}