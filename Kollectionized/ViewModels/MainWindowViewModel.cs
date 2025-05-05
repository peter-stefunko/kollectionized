using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public static string? CurrentUsername => AuthService.CurrentUsername;
    public static bool IsLoggedIn => AuthService.IsLoggedIn;

    public MainWindowViewModel()
    {
        AuthService.SessionChanged += Refresh;
    }

    [RelayCommand]
    public static void Logout()
    {
        AuthService.Logout();
    }

    private void Refresh()
    {
        OnPropertyChanged(nameof(CurrentUsername));
        OnPropertyChanged(nameof(IsLoggedIn));
    }
}