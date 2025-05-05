using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.ViewModels;

public partial class AccessWindowViewModel : ViewModelBase
{
    [ObservableProperty] private object? _currentView;

    public static bool IsLoggedIn => AuthService.IsLoggedIn;
    private readonly Action? _closeWindow;

    public string ToggleButtonText =>
        CurrentView is LoginViewModel ? "Switch to Register" : "Switch to Login";

    public AccessWindowViewModel(Action? closeWindow)
    {
        _closeWindow = closeWindow;
        CurrentView = new LoginViewModel(SwitchToRegister, _closeWindow);
    }
    
    private void SwitchToLogin()
    {
        CurrentView = new LoginViewModel(SwitchToRegister, _closeWindow);
        OnPropertyChanged(nameof(CurrentView));
        OnPropertyChanged(nameof(ToggleButtonText));
    }

    private void SwitchToRegister()
    {
        CurrentView = new RegisterViewModel(SwitchToLogin, _closeWindow);
        OnPropertyChanged(nameof(CurrentView));
        OnPropertyChanged(nameof(ToggleButtonText));
    }

    [RelayCommand]
    private void Logout()
    {
        AuthService.Logout();
        CurrentView = new LoginViewModel(SwitchToRegister, _closeWindow);
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(CurrentView));
        OnPropertyChanged(nameof(ToggleButtonText));
    }
}