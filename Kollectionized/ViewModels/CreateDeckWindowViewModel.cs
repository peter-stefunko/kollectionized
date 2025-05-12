using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public partial class CreateDeckWindowViewModel : ViewModelBase
{
    private readonly Action _onClose;
    private readonly string _username;

    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _description = string.Empty;

    public CreateDeckWindowViewModel(string username, Action onClose)
    {
        _username = username;
        _onClose = onClose;
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            ErrorMessage = "Deck name cannot be empty.";
            return;
        }

        var deck = new PokemonDeck
        {
            Name = Name,
            Description = Description,
            IsPublic = true,
            CreatedAt = DateTime.UtcNow
        };

        var success = await DecksService.CreateDeck(_username, deck);
        if (success)
            _onClose();
        else
            ErrorMessage = "Failed to create deck.";
    }

    [RelayCommand]
    private void Cancel() => _onClose();
}