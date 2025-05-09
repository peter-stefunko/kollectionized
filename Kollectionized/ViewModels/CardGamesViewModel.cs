using System.Collections.ObjectModel;
using Kollectionized.Common;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public class CardGamesViewModel : ViewModelBase
{
    public ObservableCollection<CardGameOptionViewModel> Games { get; } =
    [
        new(
            gameKey: Constants.Games.Pokemon,
            name: "Pokémon",
            assetPath: Constants.Assets.PokemonCardBack,
            openAction: () => new CardGridBrowserWindow(Constants.Games.Pokemon).Show()
        )
    ];
}