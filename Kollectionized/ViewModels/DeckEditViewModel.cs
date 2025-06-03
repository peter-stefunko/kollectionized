using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Utils;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public partial class DeckEditViewModel : ViewModelBase
{
    private readonly PokemonDeck _deck;

    [ObservableProperty] private string _editableName;
    [ObservableProperty] private string _editableDescription;
    [ObservableProperty] private string? _errorMessage;

    public DeckEditViewModel(PokemonDeck deck)
    {
        _deck = deck;
        EditableName = deck.Name;
        EditableDescription = deck.Description;
    }

    [RelayCommand]
    private async Task SaveChangesAsync()
    {
        _deck.Name = EditableName;
        _deck.Description = EditableDescription;

        var success = await DecksService.UpdateDeck(_deck.User?.Username ?? "", _deck.Id, _deck);
        if (success)
        {
            
            AppNavigation.GoBack();
        }
        else
        {
            ErrorMessage = "Failed to save changes.";
        }
    }

    [RelayCommand]
    private void Cancel() => AppNavigation.GoBack();

    [RelayCommand]
    private async Task DeleteAsync()
    {
        var result = await MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ContentTitle = "Confirm Deletion",
            ContentMessage = "Are you sure you want to delete this deck?",
            ButtonDefinitions = ButtonEnum.YesNo,
            Icon = Icon.Warning
        }).ShowAsync();

        if (result == ButtonResult.Yes)
        {
            var deleted = await DecksService.DeleteDeck(_deck.User?.Username ?? "", _deck.Id);
            if (deleted) AppNavigation.GoBack();
            else ErrorMessage = "Failed to delete deck.";
        }
    }
}