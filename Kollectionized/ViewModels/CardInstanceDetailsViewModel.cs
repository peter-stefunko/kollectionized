using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Common;
using Kollectionized.Models;
using Kollectionized.State;
using Kollectionized.Utils;

namespace Kollectionized.ViewModels;

public partial class CardInstanceDetailsViewModel : CardDetailsViewModel
{
    private CardInstance Instance { get; }

    public string GradeText => Instance.Grade is not null ? $"Grade: {Instance.Grade}" : "Grade: N/A";
    public string CompanyText => !string.IsNullOrWhiteSpace(Instance.GradingCompany) ? $"Company: {Instance.GradingCompany}" : "Company: N/A";
    public string NotesText => string.IsNullOrWhiteSpace(Instance.Notes) ? "No notes." : Instance.Notes;
    public string OwnerUsername => Instance.Owner.Username;

    public bool IsCurrentUser => CurrentUserState.User?.Id == Instance.CurrentOwner;

    public ObservableCollection<PokemonDeck> SelectedDecks { get; } = [];
    public ObservableCollection<DeckSelectionViewModel> DeckOptions { get; } = [];

    [ObservableProperty] private double? _selectedGrade;
    [ObservableProperty] private string? _selectedGradingCompany;
    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private bool _showEditForm;
    [ObservableProperty] private bool _showAddForm;

    public CardInstanceDetailsViewModel(PokemonCard card, CardInstance instance) : base(card)
    {
        Instance = instance;
        SelectedGrade = instance.Grade;
        SelectedGradingCompany = instance.GradingCompany;
        Notes = instance.Notes;

        _ = LoadDecksAsync();
    }

    private async Task LoadDecksAsync()
    {
        if (CurrentUser?.Username is not { } username) return;
        var decks = await DecksService.GetUserDecks(username);

        DeckOptions.Clear();
        SelectedDecks.Clear();
        foreach (var vm in decks.Select(deck => new DeckSelectionViewModel(deck, this)
                 {
                     IsSelected = false
                 }))
        {
            DeckOptions.Add(vm);
        }
    }

    [RelayCommand] private void ToggleEditForm() => ShowEditForm = !ShowEditForm;
    [RelayCommand] private void ToggleAddForm() => ShowAddForm = !ShowAddForm;

    [RelayCommand]
    private async Task SaveEditAsync()
    {
        await RunWithLoading(async () =>
        {
            var result = await UserCardService.UpdateCardInstance(
                Instance.Id, SelectedGrade, SelectedGradingCompany, Notes);
            if (result != null) ErrorMessage = result;
            else AppNavigation.GoBack();
        });
    }

    [RelayCommand]
    private async Task ConfirmAddToDeckAsync()
    {
        if (CurrentUser?.Username is not { } username) return;

        foreach (var deck in SelectedDecks)
        {
            await DecksService.AddCardToDeck(username, deck.Id, Instance.Id);
        }
        AppNavigation.GoBack();
    }

    [RelayCommand]
    private async Task DeleteInstanceAsync()
    {
        /*var confirmed = await DialogHelper.Confirm("Confirm", "Are you sure you want to delete this instance?");
        if (!confirmed) return;*/

        await RunWithLoading(async () =>
        {
            var result = await UserCardService.DeleteCardInstance(Instance.Id);
            if (result != null) ErrorMessage = result;
            else AppNavigation.GoBack();
        });
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(IsCurrentUser));
    }
}