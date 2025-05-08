using System;
using System.Collections.ObjectModel;
using Kollectionized.Helpers;
using Kollectionized.Models;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public class CardGamesViewModel : ViewModelBase
{
    public ObservableCollection<CardGameOption> Games { get; } =
    [
        new(
            gameKey: "pokemon",
            name: "Pokémon",
            assetPath: "avares://Kollectionized/Assets/pokemon-card-back.jpg",
            openAction: () => new CardGridBrowserWindow("pokemon").Show()
        )
    ];
}