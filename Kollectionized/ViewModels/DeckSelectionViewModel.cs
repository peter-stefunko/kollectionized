using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public class DeckSelectionViewModel(PokemonDeck deck, CardInstanceDetailsViewModel parent) : ViewModelBase
{
    public PokemonDeck Deck { get; } = deck;

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (!SetProperty(ref _isSelected, value))
                return;

            switch (value)
            {
                case true when !parent.SelectedDecks.Contains(Deck):
                    parent.SelectedDecks.Add(Deck);
                    break;
                case false when parent.SelectedDecks.Contains(Deck):
                    parent.SelectedDecks.Remove(Deck);
                    break;
            }
        }
    }
}