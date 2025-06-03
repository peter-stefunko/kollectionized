using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using System.Threading.Tasks;
using Kollectionized.Utils;

namespace Kollectionized.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string? _errorMessage;
    
    [RelayCommand]
    private async Task LoginAsync()
    {
        await RunWithLoading(async () =>
        {
            var success = await AuthService.TryLogin(Username, Password);
            if (!success)
            {
                ErrorMessage = "Invalid username or password.";
                return;
            }

            AppNavigation.GoBack();
        });
    }
}