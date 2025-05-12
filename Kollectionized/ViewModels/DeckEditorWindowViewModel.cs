using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Kollectionized.ViewModels;

public partial class DeckEditorWindowViewModel : ViewModelBase
{
    private readonly PokemonDeck _deck;
    private readonly Action _onClose;
    private readonly Action _onDelete;

    [ObservableProperty] private string _editableName;
    [ObservableProperty] private string _editableDescription;

    public DeckEditorWindowViewModel(PokemonDeck deck, Action onClose, Action onDelete)
    {
        _deck = deck;
        _onClose = onClose;
        _onDelete = onDelete;

        EditableName = deck.Name;
        EditableDescription = deck.Description;
    }

    [RelayCommand]
    private async Task SaveChangesAsync()
    {
        _deck.Name = EditableName;
        _deck.Description = EditableDescription;

        var success = await DecksService.UpdateDeck(_deck.User?.Username ?? "", _deck.Id, _deck);
        if (success) _onClose();
        else ErrorMessage = "Failed to save changes.";
    }

    [RelayCommand]
    private void Cancel() => _onClose();

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
            if (deleted) _onDelete();
            else ErrorMessage = "Failed to delete deck.";
        }
    }
}