using System.Collections.ObjectModel;
using Kollectionized.Common;

namespace Kollectionized.ViewModels;

public class CardGamesViewModel : ViewModelBase
{
    public ObservableCollection<CardGameOptionViewModel> Games { get; } =
    [
        new(
            gameKey: Constants.Games.Pokemon,
            name: "Pokémon",
            assetPath: Constants.Assets.PokemonCardBack
        )
    ];
}