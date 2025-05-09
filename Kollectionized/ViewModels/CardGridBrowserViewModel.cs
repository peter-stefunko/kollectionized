using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class CardGridBrowserViewModel : ViewModelBase
{
    private readonly string _gameKey;
    private readonly CardService _cardService;
    private readonly CardImageService _imageService;

    [ObservableProperty] private string _searchQuery = string.Empty;
    [ObservableProperty] private ObservableCollection<CardItemViewModel> _cards = new();

    [ObservableProperty] private string _nameQuery = string.Empty;

    [ObservableProperty] private ObservableCollection<string> _types = new();
    [ObservableProperty] private string? _selectedType;

    [ObservableProperty] private ObservableCollection<string> _typings = new();
    [ObservableProperty] private string? _selectedTyping;

    [ObservableProperty] private ObservableCollection<string> _forms = new();
    [ObservableProperty] private string? _selectedForm;

    [ObservableProperty] private ObservableCollection<string> _sets = new();
    [ObservableProperty] private string? _selectedSet;

    [ObservableProperty] private int _page = 0;
    private const int PageSize = 30;

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
            var cards = await _cardService.GetPokemonCards(filter);
            foreach (var card in cards)
            {
                Cards.Add(new CardItemViewModel(card, _imageService));
            }

            Page++;
        });
    }

    private void LoadFilterOptions()
    {
        Types = new(["", "Energy", "Pokémon", "Trainer"]);
        Typings = new(["", "Colorless", "Darkness", "Dragon", "Grass", "Fairy", "Fire", "Fighting", "Lightning", "Metal", "Psychic", "Water"]);
        Forms = new(["", "Baby", "Basic", "BREAK", "EX", "Fusion Strike", "Goldenrod Game Corner", "GX", "Item", "LEGEND", "Level-Up", "MEGA", "Pokémon Tool", "Pokémon Tool F", "Radiant", "Rapid Strike", "Restored", "Rocket's Secret Machine", "Single Strike", "Stadium", "Stage 1", "Stage 2", "Supporter", "TAG TEAM", "Technical Machine", "V", "VMAX"]);
        Sets = new(["", "151", "Ancient Origins", "Aquapolis", "Arceus", "Astral Radiance", "Astral Radiance Trainer Gallery", "Base", "Base Set 2", "Battle Styles", "Best of Game", "Black & White", "Boundaries Crossed", "BREAKpoint", "BREAKthrough", "Brilliant Stars", "Brilliant Stars Trainer Gallery", "Burning Shadows", "BW Black Star Promos", "Call of Legends", "Celebrations", "Celebrations: Classic Collection", "Celestial Storm", "Champion's Path", "Chilling Reign", "Cosmic Eclipse", "Crimson Invasion", "Crown Zenith", "Crown Zenith Galarian Gallery", "Crystal Guardians", "Dark Explorers", "Darkness Ablaze", "Delta Species", "Deoxys", "Detective Pikachu", "Diamond & Pearl", "Double Crisis", "DP Black Star Promos", "Dragon", "Dragon Frontiers", "Dragon Majesty", "Dragon Vault", "Dragons Exalted", "Emerald", "Emerging Powers", "Evolutions", "Evolving Skies", "EX Trainer Kit 2 Minun", "EX Trainer Kit 2 Plusle", "EX Trainer Kit Latias", "EX Trainer Kit Latios", "Expedition Base Set", "Fates Collide", "FireRed & LeafGreen", "Flashfire", "Forbidden Light", "Fossil", "Furious Fists", "Fusion Strike", "Generations", "Genetic Apex", "Great Encounters", "Guardians Rising", "Gym Challenge", "Gym Heroes", "HeartGold & SoulSilver", "HGSS Black Star Promos", "Hidden Fates", "Hidden Legends", "Holon Phantoms", "HS—Triumphant", "HS—Undaunted", "HS—Unleashed", "Journey Together", "Jungle", "Kalos Starter Set", "Legend Maker", "Legendary Collection", "Legendary Treasures", "Legends Awakened", "Lost Origin", "Lost Origin Trainer Gallery", "Lost Thunder", "Majestic Dawn", "McDonald's Collection 2011", "McDonald's Collection 2012", "McDonald's Collection 2014", "McDonald's Collection 2015", "McDonald's Collection 2016", "McDonald's Collection 2017", "McDonald's Collection 2018", "McDonald's Collection 2019", "McDonald's Collection 2021", "McDonald's Collection 2022", "Mysterious Treasures", "Mythical Island", "Neo Destiny", "Neo Discovery", "Neo Genesis", "Neo Revelation", "Next Destinies", "Nintendo Black Star Promos", "Noble Victories", "Obsidian Flames", "Paldea Evolved", "Paldean Fates", "Paradox Rift", "Phantom Forces", "Plasma Blast", "Plasma Freeze", "Plasma Storm", "Platinum", "Pokémon Futsal Collection", "Pokémon GO", "Pokémon Rumble", "POP Series 1", "POP Series 2", "POP Series 3", "POP Series 4", "POP Series 5", "POP Series 6", "POP Series 7", "POP Series 8", "POP Series 9", "Power Keepers", "Primal Clash", "Prismatic Evolutions", "Promo-A", "Rebel Clash", "Rising Rivals", "Roaring Skies", "Ruby & Sapphire", "Sandstorm", "Scarlet & Violet", "Scarlet & Violet Black Star Promos", "Scarlet & Violet Energies", "Scarlet & Violet Promos", "Secret Wonders", "Shining Fates", "Shining Legends", "Shiny Vault", "Shrouded Fable", "Silver Tempest", "Silver Tempest Trainer Gallery", "Skyridge", "SM Black Star Promos", "Southern Islands", "Space-Time Smackdown", "Steam Siege", "Stellar Crown", "Stormfront", "Sun & Moon", "Supreme Victors", "Surging Sparks", "Sword & Shield", "SWSH Black Star Promos", "Team Magma vs Team Aqua", "Team Rocket", "Team Rocket Returns", "Team Up", "Temporal Forces", "Twilight Masquerade", "Ultra Prism", "Unbroken Bonds", "Unified Minds", "Unseen Forces", "Vivid Voltage", "Wizards Black Star Promos", "XY", "XY Black Star Promos"]);
    }
}