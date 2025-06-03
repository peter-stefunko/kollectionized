using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.State;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public partial class UserProfileViewModel : ViewModelBase
{
    public User User { get; private set; }
    public bool IsCurrentUser => User.Id == CurrentUserState.User?.Id;

    private UserCardInstanceBrowserViewModel CardGridViewModel { get; }

    [ObservableProperty] private ObservableCollection<PokemonDeck> _userDecks = new();

    [ObservableProperty] private PokemonDeck? _selectedDeck;

    [ObservableProperty] private DeckBrowserViewModel? _selectedDeckViewModel;

    [ObservableProperty] private object? _currentContentView;

    [ObservableProperty] private ViewMode _selectedViewMode = ViewMode.Cards;

    public bool IsDeckView => SelectedViewMode == ViewMode.Decks;

    private readonly Action? _onUserNotFound;

    private Dictionary<Guid, DeckBrowserViewModel> _deckViewModels = new();

    public UserProfileViewModel(User user, Action? onUserNotFound = null)
    {
        User = user;
        _onUserNotFound = onUserNotFound;

        CardGridViewModel = new UserCardInstanceBrowserViewModel(user);
        CurrentContentView = CardGridViewModel;
    }
    
    partial void OnSelectedViewModeChanged(ViewMode oldValue, ViewMode newValue)
    {
        OnPropertyChanged(nameof(IsDeckView));
    }

    public async Task InitializeAsync()
    {
        await RunWithLoading(async () =>
        {
            var userInstances = await UserCardService.GetUserCardInstances(User.Username);
            var decks = await DecksService.GetUserDecks(User.Username);
            
            CardGridViewModel.LoadInstances(userInstances);
            
            UserDecks = new ObservableCollection<PokemonDeck>(decks);
            _deckViewModels = decks
                .Where(d => d.CardInstances.Any(i => i.Card != null))
                .ToDictionary(d => d.Id, d => new DeckBrowserViewModel(d));
            
            if (SelectedViewMode == ViewMode.Decks)
            {
                SelectedDeck = UserDecks.FirstOrDefault();
            }
            else
            {
                CurrentContentView = CardGridViewModel;
            }
        });
    }

    partial void OnSelectedDeckChanged(PokemonDeck? value)
    {
        if (value is not null && _deckViewModels.TryGetValue(value.Id, out var vm))
        {
            SelectedDeckViewModel = vm;
            CurrentContentView = vm;
        }
        else
        {
            SelectedDeckViewModel = null;
            CurrentContentView = null;
        }
    }

    private void Refresh(User updatedUser)
    {
        User = updatedUser;
        OnPropertyChanged(nameof(User));
        OnPropertyChanged(nameof(IsCurrentUser));
    }

    [RelayCommand]
    private async Task RefreshAllAsync()
    {
        await RunWithLoading(async () =>
        {
            var latest = await UserService.GetUserById(User.Id);
            if (latest == null)
            {
                await ShowUserNotFoundAndCloseWindow();
                return;
            }

            Refresh(latest);
            await CardGridViewModel.RefreshInstancesAsync();

            if (SelectedDeck != null)
            {
                var refreshed = await DecksService.GetDeckById(User.Username, SelectedDeck.Id);
                if (refreshed != null)
                {
                    SelectedDeck = refreshed;
                    _deckViewModels[refreshed.Id] = new DeckBrowserViewModel(refreshed);
                }
            }
        });
    }

    [RelayCommand]
    private void SwitchToCards()
    {
        SelectedViewMode = ViewMode.Cards;
        CurrentContentView = CardGridViewModel;
    }

    [RelayCommand]
    private async Task SwitchToDecksAsync()
    {
        SelectedViewMode = ViewMode.Decks;
        
        if (UserDecks.Count > 0)
        {
            var firstDeck = UserDecks.FirstOrDefault();
            if (SelectedDeck != firstDeck)
                SelectedDeck = firstDeck;
            else
                OnSelectedDeckChanged(firstDeck);
            return;
        }


        await RunWithLoading(async () =>
        {
            var decks = await DecksService.GetUserDecks(User.Username);
            UserDecks = new ObservableCollection<PokemonDeck>(decks);
            _deckViewModels = decks
                .Where(d => d.CardInstances.Any(i => i.Card != null))
                .ToDictionary(d => d.Id, d => new DeckBrowserViewModel(d));

            SelectedDeck = UserDecks.FirstOrDefault();
        });
    }

    [RelayCommand]
    private async Task DeleteSelectedDeckAsync()
    {
        if (SelectedDeck == null) return;

        var confirm = await MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ContentTitle = "Confirm",
            ContentMessage = $"Are you sure you want to delete the deck '{SelectedDeck.Name}'?",
            ButtonDefinitions = ButtonEnum.YesNo,
            Icon = Icon.Warning
        }).ShowAsync();

        if (confirm == ButtonResult.Yes)
        {
            await RunWithLoading(async () =>
            {
                var success = await DecksService.DeleteDeck(User.Username, SelectedDeck.Id);
                if (success)
                {
                    UserDecks.Remove(SelectedDeck);
                    _deckViewModels.Remove(SelectedDeck.Id);
                    SelectedDeck = UserDecks.FirstOrDefault();
                }
            });
        }
    }

    private async Task ShowUserNotFoundAndCloseWindow()
    {
        var box = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ContentTitle = "User Not Found",
            ContentMessage = "This user no longer exists.",
            ButtonDefinitions = ButtonEnum.Ok,
            Icon = Icon.Error
        });

        await box.ShowAsync();
        _onUserNotFound?.Invoke();
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(IsCurrentUser));
        CardGridViewModel.NotifySessionChanged();
        SelectedDeckViewModel?.NotifySessionChanged();
    }
}

public enum ViewMode
{
    Cards,
    Decks,
    DisplayBoards
}
