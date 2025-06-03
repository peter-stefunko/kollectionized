using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Kollectionized.Common;

namespace Kollectionized.ViewModels;

public partial class MenuWindowBase : ViewModelBase
{
    public ObservableCollection<double?> GradeOptions { get; } = new(Constants.GradeOptions);
    public ObservableCollection<string> GradingCompanies { get; } = new(Constants.GradingCompanies);

    [ObservableProperty] private double? _selectedGrade;
    [ObservableProperty] private string? _selectedGradingCompany;
    [ObservableProperty] private string _notes = string.Empty;
}