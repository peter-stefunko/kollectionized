using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public class CardInstanceItemViewModel(PokemonCard card, CardInstance instance, System.Action? onDeleted)
    : CardInstanceDetailsViewModel(card, instance, onDeleted)
{
    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(IsCurrentUser));
        EditCommand.NotifyCanExecuteChanged();
        DeleteCommand.NotifyCanExecuteChanged();
    }
}