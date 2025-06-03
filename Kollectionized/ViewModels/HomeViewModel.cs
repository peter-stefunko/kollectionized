using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using Kollectionized.State;
using Kollectionized.Utils;
using Kollectionized.Views;
using UserProfileView = Kollectionized.Views.UserProfileView;

namespace Kollectionized.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    public string? Username => CurrentUser?.Username;
    
    public HomeViewModel()
    {
        CurrentUserState.SessionChanged += NotifySessionChanged;
    }

    [RelayCommand]
    private void Logout()
    {
        AuthService.Logout();
    }

    [RelayCommand]
    private void OpenProfile()
    {
        if (CurrentUser is { } user)
            AppNavigation.NavigateTo(new UserProfileView(user));
    }

    [RelayCommand]
    private void OpenLogin()
    {
        AppNavigation.NavigateTo(new AccessView());
    }

    [RelayCommand]
    private void OpenBrowse()
    {
        AppNavigation.NavigateTo(new AllCardsView());
    }

    [RelayCommand]
    private void OpenUserSearch()
    {
        AppNavigation.NavigateTo(new UserSearchView());
    }
}