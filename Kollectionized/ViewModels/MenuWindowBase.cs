using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class MenuWindowBase : ViewModelBase
{
    public ObservableCollection<double> GradeOptions { get; } =
        new(Enumerable.Range(0, 21).Select(i => i * 0.5));

    public ObservableCollection<string> GradingCompanies { get; } =
        new() { "", "PSA", "CGC", "BGS", "ACE" };

    [ObservableProperty] private double? _selectedGrade;
    [ObservableProperty] private string? _selectedGradingCompany;
    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    protected readonly Action? _onClose;

    public MenuWindowBase(Action? onClose = null)
    {
        _onClose = onClose;
    }

    [RelayCommand]
    protected void Cancel() => _onClose?.Invoke();

    protected bool ValidateInputs()
    {
        if (Notes.Length > 100)
        {
            ErrorMessage = "Notes must be 100 characters or less.";
            return false;
        }
        return true;
    }
}