using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public class CardInstanceItemViewModel : CardInstanceDetailsViewModel
{
    public CardInstanceItemViewModel(PokemonCard card, CardInstance instance, System.Action? onDeleted)
        : base(card, instance, onDeleted) { }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(IsCurrentUser));
        EditCommand.NotifyCanExecuteChanged();
        DeleteCommand.NotifyCanExecuteChanged();
    }
}