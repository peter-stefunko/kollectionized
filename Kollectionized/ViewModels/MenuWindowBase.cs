using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Common;

namespace Kollectionized.ViewModels;

public partial class MenuWindowBase : ViewModelBase
{
    public ObservableCollection<double?> GradeOptions { get; } = new(Constants.GradeOptions);
    public ObservableCollection<string> GradingCompanies { get; } = new(Constants.GradingCompanies);

    [ObservableProperty] private double? _selectedGrade;
    [ObservableProperty] private string? _selectedGradingCompany;
    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    protected readonly Action? OnClose;

    protected MenuWindowBase(Action? onClose = null)
    {
        OnClose = onClose;
    }

    [RelayCommand]
    private void Cancel() => OnClose?.Invoke();

    protected bool ValidateInputs()
    {
        if (Notes.Length <= 100) return true;
        
        ErrorMessage = "Notes must be 100 characters or less.";
        return false;
    }
}