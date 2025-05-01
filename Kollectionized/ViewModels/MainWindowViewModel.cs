using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public string? CurrentUsername => AuthService.CurrentUsername;

    public bool IsLoggedIn => AuthService.IsLoggedIn;

    [RelayCommand]
    public void Logout()
    {
        AuthService.Logout();
        OnPropertyChanged(nameof(CurrentUsername));
        OnPropertyChanged(nameof(IsLoggedIn));
    }
    
    public void Refresh()
    {
        OnPropertyChanged(nameof(CurrentUsername));
        OnPropertyChanged(nameof(IsLoggedIn));
    }

}