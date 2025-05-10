using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class CardInstanceGridBrowserViewModel : CardFilterBaseViewModel
{
    private readonly UserCardService _userCardService = new();
    private readonly CardImageService _imageService = new();
    private readonly User _viewedUser;

    [ObservableProperty] private ObservableCollection<CardInstanceItemViewModel> _cardInstances = new();

    public ObservableCollection<double> GradeOptions { get; } =
        new(Enumerable.Range(0, 21).Select(i => i * 0.5));

    [ObservableProperty] private double? _selectedGrade;

    public ObservableCollection<string> GradingCompanies { get; } =
        new() { "", "PSA", "CGC", "BGS", "ACE" };

    [ObservableProperty] private string? _selectedGradingCompany;

    private List<CardInstance> _allInstances = [];

    public bool IsCurrentUser => _viewedUser.Id == AuthService.CurrentUser?.Id;

    public CardInstanceGridBrowserViewModel(User viewedUser)
    {
        _viewedUser = viewedUser;
        LoadFilterOptions();
        _ = LoadInstancesAsync();
    }

    private async Task LoadInstancesAsync()
    {
        await RunWithLoading(async () =>
        {
            _allInstances = await _userCardService.GetUserCardInstances(_viewedUser.Username);
            ApplyFilters();
        });
    }

    private void ApplyFilters()
    {
        var filtered = _allInstances.Where(i =>
            (string.IsNullOrWhiteSpace(NameQuery) || i.Card.Name.Contains(NameQuery, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrWhiteSpace(SelectedType) || i.Card.Type == SelectedType) &&
            (string.IsNullOrWhiteSpace(SelectedTyping) || i.Card.Typings.Contains(SelectedTyping)) &&
            (string.IsNullOrWhiteSpace(SelectedForm) || i.Card.Form == SelectedForm) &&
            (string.IsNullOrWhiteSpace(SelectedSet) || i.Card.Set == SelectedSet) &&
            (!SelectedGrade.HasValue || i.Grade == SelectedGrade) &&
            (string.IsNullOrWhiteSpace(SelectedGradingCompany) || i.GradingCompany == SelectedGradingCompany)
        );

        CardInstances = new ObservableCollection<CardInstanceItemViewModel>(
            filtered.Select(i => new CardInstanceItemViewModel(i.Card, i, _imageService)));
    }

    [RelayCommand]
    private void Search()
    {
        ApplyFilters();
    }
}