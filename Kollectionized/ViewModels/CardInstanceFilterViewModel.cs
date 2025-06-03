using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Common;

namespace Kollectionized.ViewModels;

public abstract partial class CardInstanceFilterViewModel : CardFilterBaseViewModel
{
    [ObservableProperty] private double? _selectedGrade;
    [ObservableProperty] private string? _selectedCompany;

    public ObservableCollection<double?> GradeOptions { get; } = new(Constants.GradeOptions);
    public ObservableCollection<string> GradingCompanies { get; } = new(Constants.GradingCompanies);
}