using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Utils;

namespace Kollectionized.ViewModels;

public partial class DeckCreateViewModel : ViewModelBase
{
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string? _errorMessage;

    [RelayCommand]
    private async Task CreateAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            ErrorMessage = "Deck name cannot be empty.";
            return;
        }

        if (CurrentUser?.Username is not { } username)
        {
            ErrorMessage = "You must be logged in to create a deck.";
            return;
        }

        var deck = new PokemonDeck
        {
            Name = Name,
            Description = Description,
            IsPublic = true,
            CreatedAt = DateTime.UtcNow
        };

        var success = await DecksService.CreateDeck(username, deck);
        if (success) AppNavigation.GoBack();
        else ErrorMessage = "Failed to create deck.";
    }

    [RelayCommand]
    private void Cancel() => AppNavigation.GoBack();
}