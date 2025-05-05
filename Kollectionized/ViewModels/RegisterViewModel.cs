using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class RegisterViewModel(Action? switchToLogin = null, Action? onRegisterSuccess = null)
    : ViewModelBase
{
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;
    [ObservableProperty] private string? _errorMessage = string.Empty;

    private readonly UserService _userService = new();

    [RelayCommand]
    private async Task Register()
    {
        ErrorMessage = null;

        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Passwords do not match.";
            return;
        }

        var error = await _userService.Register(Username, Password);
        if (error != null)
        {
            ErrorMessage = error;
            return;
        }

        var user = await _userService.Login(Username, Password);
        if (user != null)
        {
            AuthService.Login(user.Id, user.Username);
        }

        ErrorMessage = string.Empty;
        onRegisterSuccess?.Invoke();
    }

    [RelayCommand]
    private void SwitchToLogin()
    {
        switchToLogin?.Invoke();
    }
}