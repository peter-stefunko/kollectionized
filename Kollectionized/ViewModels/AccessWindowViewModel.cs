using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Kollectionized.Services;
using Kollectionized.ViewModels;

namespace Kollectionized.ViewModels;

public partial class AccessWindowViewModel : ObservableObject
{
    [ObservableProperty] private object? currentView;

    public bool IsLoggedIn => AuthService.IsLoggedIn;

    public string ToggleButtonText => CurrentView is LoginViewModel ? "Switch to Register" : "Switch to Login";

    public AccessWindowViewModel()
    {
        CurrentView = new LoginViewModel(SwitchToRegister, OnLoginSuccess);
    }

    private void OnLoginSuccess()
    {
        // For example, close the window or swap to another view
        OnPropertyChanged(nameof(IsLoggedIn));
        CurrentView = null; // or switch to a "welcome" screen or close the window
        OnPropertyChanged(nameof(CurrentView));
        OnPropertyChanged(nameof(ToggleButtonText));
    }
    
    private void SwitchToLogin()
    {
        CurrentView = new LoginViewModel(SwitchToRegister, OnLoginSuccess);
        OnPropertyChanged(nameof(CurrentView));
        OnPropertyChanged(nameof(ToggleButtonText));
    }

    private void SwitchToRegister()
    {
        CurrentView = new RegisterViewModel(SwitchToLogin);
        OnPropertyChanged(nameof(CurrentView));
        OnPropertyChanged(nameof(ToggleButtonText));
    }

    [RelayCommand]
    private void ToggleView()
    {
        if (CurrentView is LoginViewModel)
            SwitchToRegister();
        else
            SwitchToLogin();
    }

    [RelayCommand]
    private void Logout()
    {
        AuthService.Logout();
        OnPropertyChanged(nameof(IsLoggedIn));
    }
}