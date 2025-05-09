using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using System;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class LoginViewModel(Action? switchToRegister = null, Action? onLoginSuccess = null)
    : ViewModelBase
{
    private readonly UserService _userService = null!;

    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;

    public LoginViewModel(UserService userService, Action? switchToRegister = null, Action? onLoginSuccess = null)
        : this(switchToRegister, onLoginSuccess)
    {
        _userService = userService;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        await RunWithLoading(async () =>
        {
            var user = await _userService.Login(Username, Password);
            if (user == null)
            {
                ErrorMessage = "Invalid username or password.";
                return;
            }

            AuthService.Login(user);
            onLoginSuccess?.Invoke();
        });
    }

    [RelayCommand]
    private void SwitchToRegister()
    {
        switchToRegister?.Invoke();
    }
}