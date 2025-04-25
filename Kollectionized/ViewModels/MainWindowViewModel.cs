using Kollectionized.Models;
using Kollectionized.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Kollectionized.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly UserService _userService = new();

    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _statusMessage = string.Empty;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public string WelcomeMessage => SessionService.IsLoggedIn
        ? $"Logged in as {SessionService.CurrentUser!.Username}"
        : "Not logged in";

    public ICommand RegisterCommand => new AsyncRelayCommand(RegisterAsync);
    public ICommand LoginCommand => new AsyncRelayCommand(LoginAsync);
    public ICommand LogoutCommand => new RelayCommand(Logout);
    public ICommand DeleteAccountCommand => new AsyncRelayCommand(DeleteAccountAsync);

    public async Task RegisterAsync()
    {
        var error = await _userService.Register(Username, Password);
        StatusMessage = error ?? "User registered successfully.";
    }

    public async Task LoginAsync()
    {
        var user = await _userService.Login(Username, Password);
        if (user != null)
        {
            SessionService.SetUser(user);
            StatusMessage = $"Welcome, {user.Username}!";
            OnPropertyChanged(nameof(WelcomeMessage));
        }
        else
        {
            StatusMessage = "Invalid username or password.";
        }
    }

    public void Logout()
    {
        SessionService.Logout();
        Username = string.Empty;
        Password = string.Empty;
        StatusMessage = "Logged out.";
        OnPropertyChanged(nameof(WelcomeMessage));
    }

    public async Task DeleteAccountAsync()
    {
        if (!SessionService.IsLoggedIn)
        {
            StatusMessage = "You must be logged in to delete your account.";
            return;
        }

        bool success = await _userService.DeleteAccount(SessionService.CurrentUser!.Username, Password);
        if (success)
        {
            Logout();
            StatusMessage = "Account deleted.";
        }
        else
        {
            StatusMessage = "Incorrect password. Could not delete account.";
        }
    }
}
