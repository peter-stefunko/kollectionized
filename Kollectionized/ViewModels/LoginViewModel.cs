using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using System;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class LoginViewModel(Action? switchToRegister, Action? onLoginSuccess)
    : ViewModelBase
{
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    
    [RelayCommand]
    private async Task LoginAsync()
    {
        await RunWithLoading(async () =>
        {
            var user = await UserService.Login(Username, Password);
            if (user == null)
            {
                ErrorMessage = "Invalid username or password.";
                return;
            }

            AuthService.Login(user, Password);
            onLoginSuccess?.Invoke();
        });
    }

    [RelayCommand]
    private void SwitchToRegister()
    {
        switchToRegister?.Invoke();
    }
}