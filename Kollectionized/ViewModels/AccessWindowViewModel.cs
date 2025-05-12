using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class AccessWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? _currentView;
    private readonly Action _onSuccess;

    public string ToggleButtonText => CurrentView is LoginViewModel
        ? "Switch to Register"
        : "Switch to Login";

    public AccessWindowViewModel(Action onSuccess)
    {
        _onSuccess = onSuccess;
        ShowLogin();
    }

    [RelayCommand]
    private void ToggleView()
    {
        if (CurrentView is LoginViewModel)
            ShowRegister();
        else
            ShowLogin();

        OnPropertyChanged(nameof(ToggleButtonText));
    }

    [RelayCommand]
    private void Logout()
    {
        AuthService.Logout();
        ShowLogin();
        OnPropertyChanged(nameof(ToggleButtonText));
    }

    private void ShowLogin() =>
        CurrentView = new LoginViewModel(ShowRegister, _onSuccess);

    private void ShowRegister() =>
        CurrentView = new RegisterViewModel(ShowLogin, _onSuccess);
}