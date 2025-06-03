using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;

namespace Kollectionized.ViewModels;

public partial class CardGridBrowserViewModel : CardFilterBaseViewModel
{
    [ObservableProperty] private ObservableCollection<CardItemViewModel> _cards = [];

    [ObservableProperty] private int _page;
    private const int PageSize = 30;
    
    public CardGridBrowserViewModel()
    {
        _ = SearchAsync();
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        Page = 0;
        Cards.Clear();
        await LoadCardsAsync();
    }

    [RelayCommand]
    private async Task LoadCardsAsync()
    {
        var filter = new PokemonCardFilter
        {
            Name = NameQuery,
            Type = SelectedType,
            Typing = SelectedTyping,
            Form = SelectedForm,
            Set = SelectedSet,
            Limit = PageSize,
            Offset = Page * PageSize,
        };

        await RunWithLoading(async () =>
        {
            var cards = await CardService.GetPokemonCards(filter);
            foreach (var card in cards)
            {
                Cards.Add(new CardItemViewModel(card));
            }
            Page++;
        });
    }

    public override void NotifySessionChanged()
    {
        base.NotifySessionChanged();

        foreach (var card in Cards)
        {
            card.NotifySessionChanged();
        }
    }
}