using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public partial class AddToCollectionWindowViewModel : ViewModelBase
{
    private readonly Guid _instanceId;
    private readonly string _username;
    private readonly Action _onClose;

    [ObservableProperty] private ObservableCollection<PokemonDeck> _userDecks = new();
    [ObservableProperty] private ObservableCollection<PokemonDeck> _selectedDecks = new();

    public AddToCollectionWindowViewModel(Guid instanceId, string username, Action onClose)
    {
        _instanceId = instanceId;
        _username = username;
        _onClose = onClose;
        _ = LoadAsync();
    }

    private async Task LoadAsync()
    {
        var decks = await DecksService.GetUserDecks(_username);
        UserDecks = new ObservableCollection<PokemonDeck>(decks);
    }

    [RelayCommand]
    private async Task ConfirmAsync()
    {
        foreach (var deck in SelectedDecks)
        {
            await DecksService.AddCardToDeck(_username, deck.Id, _instanceId);
        }
        _onClose();
    }

    [RelayCommand]
    private void Cancel() => _onClose();
}