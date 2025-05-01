using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class RegisterViewModel : ObservableObject
{
    private readonly Action? _switchToLogin;

    public RegisterViewModel(Action? switchToLogin = null)
    {
        _switchToLogin = switchToLogin;
    }

    [ObservableProperty] private string username = string.Empty;
    [ObservableProperty] private string password = string.Empty;
    [ObservableProperty] private string confirmPassword = string.Empty;
    [ObservableProperty] private string? errorMessage;

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
            AuthService.Login(user.Id, user.Username);
    }

    [RelayCommand]
    private void SwitchToLogin()
    {
        _switchToLogin?.Invoke();
    }
}