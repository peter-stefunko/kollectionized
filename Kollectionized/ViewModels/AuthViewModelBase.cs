using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public abstract partial class AuthViewModelBase : ViewModelBase
{
    protected readonly UserService UserService = new();

    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    protected bool ValidateCredentials()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Username and password are required.";
            return false;
        }
        return true;
    }
}
