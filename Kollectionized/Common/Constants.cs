using System.Linq;

namespace Kollectionized.Common;

public static class Constants
{
    public const string DefaultApiBaseUrl = "https://kollectionized-api-delicate-field-1881.fly.dev/api/";

    public static class Games
    {
        public const string Pokemon = "pokemon";
    }

    public static class Assets
    {
        public const string PokemonCardBack = "avares://Kollectionized/Assets/pokemon-card-back.jpg";
    }

    public static class Storage
    {
        public const string SupabaseBaseUrl = "https://luvbckqisrgzkrsckfqp.supabase.co/storage/v1/object/public";
    }

    public static readonly string[] GradingCompanies =
    [
        "", "PSA", "CGC", "BGS", "ACE"
    ];

    public static readonly double?[] GradeOptions =
        new double?[] { null }.Concat(Enumerable.Range(0, 21).Select(i => (double?)(i * 0.5))).ToArray();

    public static readonly string[] PokemonTypes =
    [
        "", "Energy", "Pokémon", "Trainer"
    ];

    public static readonly string[] PokemonTypings =
    [
        "", "Colorless", "Darkness", "Dragon", "Grass", "Fairy", "Fire", "Fighting", "Lightning", "Metal", "Psychic", "Water"
    ];

    public static readonly string[] PokemonForms =
    [
        "", "Baby", "Basic", "BREAK", "EX", "Fusion Strike", "Goldenrod Game Corner", "GX", "Item", "LEGEND",
        "Level-Up", "MEGA", "Pokémon Tool", "Pokémon Tool F", "Radiant", "Rapid Strike", "Restored",
        "Rocket's Secret Machine", "Single Strike", "Stadium", "Stage 1", "Stage 2", "Supporter", "TAG TEAM",
        "Technical Machine", "V", "VMAX"
    ];

    public static readonly string[] PokemonSets =
    [
        "", "151", "Ancient Origins", "Aquapolis", "Arceus", "Astral Radiance", "Astral Radiance Trainer Gallery",
        "Base", "Base Set 2", "Battle Styles", "Best of Game", "Black & White", "Boundaries Crossed", "BREAKpoint",
        "BREAKthrough", "Brilliant Stars", "Brilliant Stars Trainer Gallery", "Burning Shadows", "BW Black Star Promos",
        "Call of Legends", "Celebrations", "Celebrations: Classic Collection", "Celestial Storm", "Champion's Path",
        "Chilling Reign", "Cosmic Eclipse", "Crimson Invasion", "Crown Zenith", "Crown Zenith Galarian Gallery",
        "Crystal Guardians", "Dark Explorers", "Darkness Ablaze", "Delta Species", "Deoxys", "Detective Pikachu",
        "Diamond & Pearl", "Double Crisis", "DP Black Star Promos", "Dragon", "Dragon Frontiers", "Dragon Majesty",
        "Dragon Vault", "Dragons Exalted", "Emerald", "Emerging Powers", "Evolutions", "Evolving Skies",
        "EX Trainer Kit 2 Minun", "EX Trainer Kit 2 Plusle", "EX Trainer Kit Latias", "EX Trainer Kit Latios",
        "Expedition Base Set", "Fates Collide", "FireRed & LeafGreen", "Flashfire", "Forbidden Light", "Fossil",
        "Furious Fists", "Fusion Strike", "Generations", "Genetic Apex", "Great Encounters", "Guardians Rising",
        "Gym Challenge", "Gym Heroes", "HeartGold & SoulSilver", "HGSS Black Star Promos", "Hidden Fates",
        "Hidden Legends", "Holon Phantoms", "HS—Triumphant", "HS—Undaunted", "HS—Unleashed", "Journey Together",
        "Jungle", "Kalos Starter Set", "Legend Maker", "Legendary Collection", "Legendary Treasures",
        "Legends Awakened", "Lost Origin", "Lost Origin Trainer Gallery", "Lost Thunder", "Majestic Dawn",
        "McDonald's Collection 2011", "McDonald's Collection 2012", "McDonald's Collection 2014",
        "McDonald's Collection 2015", "McDonald's Collection 2016", "McDonald's Collection 2017",
        "McDonald's Collection 2018", "McDonald's Collection 2019", "McDonald's Collection 2021",
        "McDonald's Collection 2022", "Mysterious Treasures", "Mythical Island", "Neo Destiny", "Neo Discovery",
        "Neo Genesis", "Neo Revelation", "Next Destinies", "Nintendo Black Star Promos", "Noble Victories",
        "Obsidian Flames", "Paldea Evolved", "Paldean Fates", "Paradox Rift", "Phantom Forces", "Plasma Blast",
        "Plasma Freeze", "Plasma Storm", "Platinum", "Pokémon Futsal Collection", "Pokémon GO", "Pokémon Rumble",
        "POP Series 1", "POP Series 2", "POP Series 3", "POP Series 4", "POP Series 5", "POP Series 6",
        "POP Series 7", "POP Series 8", "POP Series 9", "Power Keepers", "Primal Clash", "Prismatic Evolutions",
        "Promo-A", "Rebel Clash", "Rising Rivals", "Roaring Skies", "Ruby & Sapphire", "Sandstorm", "Scarlet & Violet",
        "Scarlet & Violet Black Star Promos", "Scarlet & Violet Energies", "Scarlet & Violet Promos",
        "Secret Wonders", "Shining Fates", "Shining Legends", "Shiny Vault", "Shrouded Fable", "Silver Tempest",
        "Silver Tempest Trainer Gallery", "Skyridge", "SM Black Star Promos", "Southern Islands",
        "Space-Time Smackdown", "Steam Siege", "Stellar Crown", "Stormfront", "Sun & Moon", "Supreme Victors",
        "Surging Sparks", "Sword & Shield", "SWSH Black Star Promos", "Team Magma vs Team Aqua", "Team Rocket",
        "Team Rocket Returns", "Team Up", "Temporal Forces", "Twilight Masquerade", "Ultra Prism", "Unbroken Bonds",
        "Unified Minds", "Unseen Forces", "Vivid Voltage", "Wizards Black Star Promos", "XY", "XY Black Star Promos"
    ];
}