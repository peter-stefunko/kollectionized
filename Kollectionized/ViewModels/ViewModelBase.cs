using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Services;
using System;
using System.Threading.Tasks;
using Kollectionized.Models;
using Kollectionized.State;

namespace Kollectionized.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    protected static User? CurrentUser => CurrentUserState.User;
    public static bool IsLoggedIn => CurrentUserState.IsLoggedIn;

    [ObservableProperty] private string? _errorMessage;
    [ObservableProperty] private bool _isLoading;

    protected static readonly CardService CardService = new();
    protected static readonly SetsService SetsService = new();
    protected static readonly UserCardService UserCardService = new();
    protected static readonly UserService UserService = new();
    protected static readonly DecksService DecksService = new();

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
            ErrorMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public virtual void NotifySessionChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(CurrentUser));
        OnPropertyChanged(nameof(CurrentUser.Username));
    }
}