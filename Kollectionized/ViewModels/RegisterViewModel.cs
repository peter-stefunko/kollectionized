using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using System;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class RegisterViewModel(UserService userService, Action? switchToLogin = null, Action? onRegisterSuccess = null)
    : ViewModelBase
{
    private readonly UserService _userService = userService;

    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Passwords do not match.";
            return;
        }

        await RunWithLoading(async () =>
        {
            var error = await _userService.Register(Username, Password);
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            var user = await _userService.Login(Username, Password);
            if (user != null)
            {
                AuthService.Login(user);
                onRegisterSuccess?.Invoke();
            }
        });
    }

    [RelayCommand]
    private void SwitchToLogin()
    {
        switchToLogin?.Invoke();
    }
}