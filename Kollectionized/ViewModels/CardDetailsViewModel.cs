using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Common;
using Kollectionized.Models;
using Kollectionized.Services;
using Kollectionized.State;

namespace Kollectionized.ViewModels;

public partial class CardDetailsViewModel : ViewModelBase
{
    public PokemonCard Card { get; }

    [ObservableProperty] private Bitmap? _image;

    public ObservableCollection<double?> GradeOptions { get; } = new(Constants.GradeOptions);
    public ObservableCollection<string> GradingCompanies { get; } = new(Constants.GradingCompanies);

    [ObservableProperty] private double? _selectedGrade;
    [ObservableProperty] private string? _selectedGradingCompany;
    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    [ObservableProperty] private bool _showAddForm;

    public bool IsAddButtonVisible => CurrentUserState.IsLoggedIn && !ShowAddForm;
    public bool IsAddFormVisible => CurrentUserState.IsLoggedIn && ShowAddForm;

    public IRelayCommand AddCommand { get; }
    public IRelayCommand ShowAddFormCommand { get; }
    public IRelayCommand CancelAddFormCommand { get; }

    public CardDetailsViewModel(PokemonCard card)
    {
        Card = card;
        AddCommand = new RelayCommand(async () => await AddAsync());
        ShowAddFormCommand = new RelayCommand(() => ShowAddForm = true);
        CancelAddFormCommand = new RelayCommand(() => ShowAddForm = false);
        _ = LoadImageAsync();
    }

    private async Task LoadImageAsync()
    {
        Image = await CardImageService.LoadCardImageAsync(Card);
    }

    private async Task AddAsync()
    {
        if (Notes.Length > 100)
        {
            ErrorMessage = "Notes must be 100 characters or less.";
            return;
        }

        await RunWithLoading(async () =>
        {
            var error = await UserCardService.AddCardInstance(Card.Uuid, SelectedGrade, SelectedGradingCompany, Notes);

            if (error != null)
            {
                ErrorMessage = error;
            }
            else
            {
                ErrorMessage = "Card added successfully.";
                Notes = string.Empty;
                SelectedGrade = null;
                SelectedGradingCompany = null;
                ShowAddForm = false;
            }
        });
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(IsAddButtonVisible));
        OnPropertyChanged(nameof(IsAddFormVisible));
    }

    partial void OnShowAddFormChanged(bool value)
    {
        OnPropertyChanged(nameof(IsAddButtonVisible));
        OnPropertyChanged(nameof(IsAddFormVisible));
    }
}
