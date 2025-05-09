using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class AccessWindowViewModel : ViewModelBase
{
    private readonly Func<Action, ViewModelBase> _createLoginViewModel;
    private readonly Func<Action, ViewModelBase> _createRegisterViewModel;
    private readonly Action? _closeWindow;

    [ObservableProperty] private ViewModelBase? _currentView;

    public string ToggleButtonText =>
        CurrentView is LoginViewModel ? "Switch to Register" : "Switch to Login";

    public static bool IsLoggedIn => AuthService.IsLoggedIn;

    public AccessWindowViewModel(
        Func<Action, ViewModelBase> createLoginViewModel,
        Func<Action, ViewModelBase> createRegisterViewModel,
        Action? closeWindow = null)
    {
        _createLoginViewModel = createLoginViewModel;
        _createRegisterViewModel = createRegisterViewModel;
        _closeWindow = closeWindow;

        CurrentView = _createLoginViewModel.Invoke(SwitchToRegister);
    }

    private void SwitchToLogin()
    {
        CurrentView = _createLoginViewModel.Invoke(SwitchToRegister);
        OnPropertyChanged(nameof(ToggleButtonText));
    }

    private void SwitchToRegister()
    {
        CurrentView = _createRegisterViewModel.Invoke(SwitchToLogin);
        OnPropertyChanged(nameof(ToggleButtonText));
    }

    [RelayCommand]
    private void Logout()
    {
        AuthService.Logout();
        CurrentView = _createLoginViewModel.Invoke(SwitchToRegister);
        OnPropertyChanged(nameof(ToggleButtonText));
        OnPropertyChanged(nameof(IsLoggedIn));
    }
}