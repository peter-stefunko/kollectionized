using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using Kollectionized.Models;
using System;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly Action? _switchToRegister;
    private readonly Action? _onLoginSuccess;

    public LoginViewModel(Action? switchToRegister = null, Action? onLoginSuccess = null)
    {
        _switchToRegister = switchToRegister;
        _onLoginSuccess = onLoginSuccess;

    }

    [ObservableProperty] private string username = string.Empty;
    [ObservableProperty] private string password = string.Empty;
    [ObservableProperty] private string? errorMessage;

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

        AuthService.Login(user.Id, user.Username);
        _onLoginSuccess?.Invoke();
    }

    [RelayCommand]
    private void SwitchToRegister()
    {
        _switchToRegister?.Invoke();
    }
    public IRelayCommand TestCommand => new RelayCommand(() => Console.WriteLine("Clicked!"));
    
}