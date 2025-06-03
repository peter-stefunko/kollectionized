using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;
using System.Threading.Tasks;
using Kollectionized.Utils;

namespace Kollectionized.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Passwords do not match.";
            return;
        }

        await RunWithLoading(async () =>
        {
            var error = await UserService.Register(Username, Password);
            if (error != null)
            {
                ErrorMessage = error;
                return;
            }

            var success = await AuthService.TryLogin(Username, Password);
            
            if (success)
                AppNavigation.GoBack();
        });
    }
}