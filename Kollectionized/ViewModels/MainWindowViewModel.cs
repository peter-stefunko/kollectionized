using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using Kollectionized.State;

namespace Kollectionized.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    /*public string? CurrentUsername => CurrentUserState.User?.Username;
    public bool IsLoggedIn => CurrentUserState.IsLoggedIn;*/

    public MainWindowViewModel()
    {
        CurrentUserState.SessionChanged += NotifySessionChanged;
    }

    /*private void NotifySessionChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(CurrentUsername));
    }*/

    [RelayCommand]
    private void Logout()
    {
        AuthService.Logout();
    }
}