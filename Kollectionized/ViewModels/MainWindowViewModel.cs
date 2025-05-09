using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public new static string? CurrentUsername => AuthService.CurrentUser?.Username;
    public new static bool IsLoggedIn => AuthService.IsLoggedIn;

    public MainWindowViewModel()
    {
        AuthService.SessionChanged += Refresh;
    }

    [RelayCommand]
    private void Logout()
    {
        AuthService.Logout();
    }

    private void Refresh()
    {
        OnPropertyChanged(nameof(CurrentUsername));
        OnPropertyChanged(nameof(IsLoggedIn));
    }
}