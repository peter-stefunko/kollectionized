using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class CardGridBrowserViewModel : CardFilterBaseViewModel
{
    private readonly CardService _cardService;
    private readonly CardImageService _imageService;

    [ObservableProperty] private ObservableCollection<CardItemViewModel> _cards = new();

    [ObservableProperty] private int _page;
    private const int PageSize = 30;

    private readonly string _gameKey;

    public CardGridBrowserViewModel(string gameKey, CardService cardService, CardImageService imageService)
    {
        _gameKey = gameKey;
        _cardService = cardService;
        _imageService = imageService;
        _ = SearchAsync();
        LoadFilterOptions();
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        Page = 0;
        Cards.Clear();
        await RunWithLoading(LoadCardsAsync);
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
            var cards = await _cardService.GetPokemonCards(_gameKey, filter);
            foreach (var card in cards)
            {
                Cards.Add(new CardItemViewModel(card, _imageService));
            }
            Page++;
        });
    }
}