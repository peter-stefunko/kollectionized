using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    public string? CurrentUsername => AuthService.CurrentUser?.Username;
    public bool IsLoggedIn => AuthService.IsLoggedIn;

    [ObservableProperty] private string? _errorMessage;
    [ObservableProperty] private bool _isLoading;

    protected void WatchSession()
    {
        AuthService.SessionChanged += () =>
        {
            OnPropertyChanged(nameof(CurrentUsername));
            OnPropertyChanged(nameof(IsLoggedIn));
        };
    }

    protected async Task RunWithLoading(Func<Task> action)
    {
        IsLoading = true;
        ErrorMessage = null;

        try
        {
            await action();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Something went wrong.";
            Console.WriteLine(ex);
        }
        finally
        {
            IsLoading = false;
        }
    }
}