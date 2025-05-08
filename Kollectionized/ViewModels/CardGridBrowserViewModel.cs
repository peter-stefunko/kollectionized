using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public partial class CardGridBrowserViewModel : ViewModelBase
{
    private readonly string _gameKey;
    private readonly CardService _cardService = new();

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    [ObservableProperty]
    private ObservableCollection<CardItemViewModel> _cards = [];

    public CardGridBrowserViewModel(string gameKey)
    {
        _gameKey = gameKey;
        LoadCards();
        LoadFilterOptions();
        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(SearchQuery))
                OnPropertyChanged(nameof(Cards));
        };
    }

    private async void LoadCards()
    {
        var cards = _gameKey switch
        {
            "pokemon" => await _cardService.GetPokemonCards(new PokemonCardFilter()),
            _ => []
        };

        Cards = new ObservableCollection<CardItemViewModel>(
            cards.Select(c => new CardItemViewModel(c)));
        OnPropertyChanged(nameof(Cards));
    }
    
    [ObservableProperty] private string _nameQuery = string.Empty;

    [ObservableProperty] private ObservableCollection<string> _cardTypes = [];
    [ObservableProperty] private string? _selectedCardType;

    [ObservableProperty] private ObservableCollection<string> _types = [];
    [ObservableProperty] private string? _selectedType;

    [ObservableProperty] private ObservableCollection<string> _subTypes = new();
    [ObservableProperty] private string? _selectedSubType;

    [ObservableProperty] private ObservableCollection<string> _sets = [];
    [ObservableProperty] private string? _selectedSet;

    [RelayCommand]
    private async Task Search()
    {
        var filter = new PokemonCardFilter
        {
            Name = NameQuery,
            CardType = SelectedCardType,
            Type = SelectedType,
            SubType = SelectedSubType,
            Set = SelectedSet,
            Limit = 300,
            Offset = 0,
        };

        var cards = await _cardService.GetPokemonCards(filter);
        Cards = new ObservableCollection<CardItemViewModel>(
            cards.Select(c => new CardItemViewModel(c)));
    }
    
    private async void LoadFilterOptions()
    {
        CardTypes = new(["Energy", "Pokemon", "Trainer"]);
        Types = new(["Colorless", "Darkness", "Dragon", "Grass", "Fairy", "Fire", "Fighting", "Lightning", "Metal", "Psychic", "Water"]);
        SubTypes = new(["Baby", "Basic", "BREAK", "EX", "Fusion Strike", "Goldenrod Game Corner", "GX", "Item", "LEGEND", "Level-Up", "MEGA", "Pokémon Tool", "Pokémon Tool F", "Radiant", "Rapid Strike", "Restored", "Rocket's Secret Machine", "Single Strike", "Stadium", "Stage 1", "Stage 2", "Supporter", "TAG TEAM", "Technical Machine", "V", "VMAX"]);
        Sets = new(["151", "Ancient Origins", "Aquapolis", "Arceus", "Astral Radiance", "Astral Radiance Trainer Gallery", "Base", "Base Set 2", "Battle Styles", "Best of Game", "Black & White", "Boundaries Crossed", "BREAKpoint", "BREAKthrough", "Brilliant Stars", "Brilliant Stars Trainer Gallery", "Burning Shadows", "BW Black Star Promos", "Call of Legends", "Celebrations", "Celebrations: Classic Collection", "Celestial Storm", "Champion's Path", "Chilling Reign", "Cosmic Eclipse", "Crimson Invasion", "Crown Zenith", "Crown Zenith Galarian Gallery", "Crystal Guardians", "Dark Explorers", "Darkness Ablaze", "Delta Species", "Deoxys", "Detective Pikachu", "Diamond & Pearl", "Double Crisis", "DP Black Star Promos", "Dragon", "Dragon Frontiers", "Dragon Majesty", "Dragon Vault", "Dragons Exalted", "Emerald", "Emerging Powers", "Evolutions", "Evolving Skies", "EX Trainer Kit 2 Minun", "EX Trainer Kit 2 Plusle", "EX Trainer Kit Latias", "EX Trainer Kit Latios", "Expedition Base Set", "Fates Collide", "FireRed & LeafGreen", "Flashfire", "Forbidden Light", "Fossil", "Furious Fists", "Fusion Strike", "Generations", "Great Encounters", "Guardians Rising", "Gym Challenge", "Gym Heroes", "HeartGold & SoulSilver", "HGSS Black Star Promos", "Hidden Fates", "Hidden Legends", "Holon Phantoms", "HS—Triumphant", "HS—Undaunted", "HS—Unleashed", "Journey Together", "Jungle", "Kalos Starter Set", "Legend Maker", "Legendary Collection", "Legendary Treasures", "Legends Awakened", "Lost Origin", "Lost Origin Trainer Gallery", "Lost Thunder", "Majestic Dawn", "Mysterious Treasures", "Neo Destiny", "Neo Discovery", "Neo Genesis", "Neo Revelation", "Next Destinies", "Nintendo Black Star Promos", "Noble Victories", "Obsidian Flames", "Paldea Evolved", "Paldean Fates", "Paradox Rift", "Phantom Forces", "Plasma Blast", "Plasma Freeze", "Plasma Storm", "Platinum", "Pokémon Futsal Collection", "Pokémon Rumble", "Power Keepers", "Primal Clash", "Prismatic Evolutions", "Rebel Clash", "Rising Rivals", "Roaring Skies", "Ruby & Sapphire", "Sandstorm", "Scarlet & Violet", "Scarlet & Violet Black Star Promos", "Scarlet & Violet Energies", "Scarlet & Violet Promos", "Secret Wonders", "Shining Fates", "Shining Legends", "Shiny Vault", "Shrouded Fable", "Silver Tempest", "Silver Tempest Trainer Gallery", "Skyridge", "SM Black Star Promos", "Southern Islands", "Steam Siege", "Stellar Crown", "Stormfront", "Sun & Moon", "Supreme Victors", "Surging Sparks", "Sword & Shield", "SWSH Black Star Promos", "Team Magma vs Team Aqua", "Team Rocket", "Team Rocket Returns", "Team Up", "Temporal Forces", "Twilight Masquerade", "Ultra Prism", "Unbroken Bonds", "Unified Minds", "Unseen Forces", "Vivid Voltage", "Wizards Black Star Promos", "XY", "XY Black Star Promos"]);
    }
}