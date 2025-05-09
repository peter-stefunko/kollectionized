using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Kollectionized.Models;

namespace Kollectionized.Services;

public class CardService : ServiceBase
{
    public async Task<List<PokemonCard>> GetPokemonCards(string gameKey, PokemonCardFilter filter)
    {
        try
        {
            var response = await Client.PostAsJsonAsync($"cards/{gameKey}", filter);
            if (!response.IsSuccessStatusCode) return [];

            var cards = await response.Content.ReadFromJsonAsync<List<PokemonCard>>();
            return cards ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to fetch cards for game '{gameKey}': {ex.Message}");
            return [];
        }
    }
}