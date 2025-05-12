using System;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public partial class DeckBrowserViewModel : CardInstanceGridBrowserViewModel
{
    public string DeckName { get; }
    public string DeckDescription { get; }
    public DateTime DeckCreatedAt { get; }
    public string DeckOwnerUsername { get; }

    public int CardCount => CardInstances.Count;
    public string CardCountDisplay => $"{CardCount}/60 cards";

    public DeckBrowserViewModel(PokemonDeck deck)
        : base(deck.User!, skipInit: true)
    {
        DeckName = deck.Name;
        DeckDescription = deck.Description;
        DeckCreatedAt = deck.CreatedAt;
        DeckOwnerUsername = deck.User?.Username ?? "Unknown";

        AllInstances = deck.CardInstances;
        ApplyFilters();
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(CardCount));
        OnPropertyChanged(nameof(CardCountDisplay));
    }
}
