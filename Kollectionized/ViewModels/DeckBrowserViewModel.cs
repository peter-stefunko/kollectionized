using System;
using System.Linq;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public partial class DeckBrowserViewModel : UserCardInstanceBrowserViewModel
{
    public string DeckName { get; }
    public string DeckDescription { get; }
    public DateTime DeckCreatedAt { get; }
    public string DeckOwnerUsername { get; }

    public int CardCount => Instances.Count;
    public string CardCountDisplay => $"{CardCount}/60 cards";

    public DeckBrowserViewModel(PokemonDeck deck)
        : base(deck.User!)
    {
        DeckName = deck.Name;
        DeckDescription = deck.Description;
        DeckCreatedAt = deck.CreatedAt;
        DeckOwnerUsername = deck.User?.Username ?? "Unknown";
        
        AllInstances = deck.CardInstances
            .Where(i => i.Card != null)
            .Select(i => new CardInstanceItemViewModel(i.Card, i))
            .ToList();
        
        NameQuery = string.Empty;
        SelectedSet = null;
        SelectedType = null;
        SelectedForm = null;
        SelectedGrade = null;
        SelectedCompany = null;

        ApplyFilters();
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();
        OnPropertyChanged(nameof(CardCount));
        OnPropertyChanged(nameof(CardCountDisplay));
    }
}