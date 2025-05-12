using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Services;
using System;
using System.Threading.Tasks;

namespace Kollectionized.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    public static string? CurrentUsername => AuthService.CurrentUser?.Username;
    public static bool IsLoggedIn => AuthService.IsLoggedIn;

    [ObservableProperty] private string? _errorMessage;
    [ObservableProperty] private bool _isLoading;

    protected static readonly CardService CardService = new();
    protected static readonly SetsService SetsService = new();
    protected static readonly UserCardService UserCardService = new();
    protected static readonly UserService UserService = new();

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

    public virtual void NotifySessionChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(CurrentUsername));
    }
}