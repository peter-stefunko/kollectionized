using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.State;

namespace Kollectionized.ViewModels;

public partial class AccessViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? _currentView;

    private readonly LoginViewModel _loginViewModel;
    private readonly RegisterViewModel _registerViewModel;

    public string ToggleButtonText => CurrentView is LoginViewModel
        ? "Switch to Register"
        : "Switch to Login";

    public AccessViewModel()
    {
        _loginViewModel = new LoginViewModel();
        _registerViewModel = new RegisterViewModel();

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
        CurrentUserState.Logout();
        ShowLogin();
        OnPropertyChanged(nameof(ToggleButtonText));
    }

    private void ShowLogin() => CurrentView = _loginViewModel;
    private void ShowRegister() => CurrentView = _registerViewModel;
}