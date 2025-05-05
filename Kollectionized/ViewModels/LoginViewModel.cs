using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using Kollectionized.Models;
using System;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class LoginViewModel(Action? switchToRegister = null, Action? onLoginSuccess = null)
    : ViewModelBase
{
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    private readonly UserService _userService = new();

    [RelayCommand]
    private async Task Login()
    {
        ErrorMessage = null;

        var user = await _userService.Login(Username, Password);
        if (user == null)
        {
            ErrorMessage = "Invalid username or password.";
            return;
        }

        ErrorMessage = string.Empty;
        AuthService.Login(user.Id, user.Username);
        onLoginSuccess?.Invoke();
    }

    [RelayCommand]
    private void SwitchToRegister()
    {
        switchToRegister?.Invoke();
    }
}