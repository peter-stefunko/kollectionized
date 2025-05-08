using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Kollectionized.Models;

namespace Kollectionized.Services;

public class CardService
{
    private readonly HttpClient _client;

    public CardService()
    {
        var baseUrl = Environment.GetEnvironmentVariable("KOLLECTIONIZED_API")
                      ?? "https://kollectionized-api-delicate-field-1881.fly.dev/api/";
        _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public async Task<List<PokemonCard>> GetPokemonCards(PokemonCardFilter filter)
    {
        var body = JsonContent.Create(filter);
        var response = await _client.PostAsync("cards/pokemon", body);
        if (!response.IsSuccessStatusCode) return [];
        var result = await response.Content.ReadFromJsonAsync<List<PokemonCard>>();
        return result ?? [];
    }
}