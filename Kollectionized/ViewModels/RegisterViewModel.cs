using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using System;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class RegisterViewModel(Action? switchToLogin = null, Action? onRegisterSuccess = null)
    : ViewModelBase
{
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
            var error = await UserService.Register(Username, Password);
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            var user = await UserService.Login(Username, Password);
            if (user != null)
            {
                AuthService.Login(user, Password);
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