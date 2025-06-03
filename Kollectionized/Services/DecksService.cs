using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Kollectionized.Models;

namespace Kollectionized.Services;

public class DecksService : ServiceBase
{
    public async Task<List<PokemonDeck>> GetUserDecks(string username)
    {
        var response = await Client.GetAsync($"users/{username}/decks");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<PokemonDeck>>() ?? new()
            : new();
    }

    public async Task<PokemonDeck?> GetDeckById(string username, Guid deckId)
    {
        var response = await Client.GetAsync($"users/{username}/decks/{deckId}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<PokemonDeck>()
            : null;
    }

    public async Task<bool> CreateDeck(string username, PokemonDeck deck)
    {
        var response = await Client.PostAsJsonAsync($"users/{username}/decks", deck);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddCardToDeck(string username, Guid deckId, Guid instanceId)
    {
        var response = await Client.PostAsJsonAsync($"users/{username}/decks/{deckId}/cards", instanceId);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveCardFromDeck(string username, Guid deckId, Guid instanceId)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"users/{username}/decks/{deckId}/cards")
        {
            Content = JsonContent.Create(instanceId)
        };
        var response = await Client.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteDeck(string username, Guid deckId)
    {
        var response = await Client.DeleteAsync($"users/{username}/decks/{deckId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateDeck(string username, Guid deckId, PokemonDeck deck)
    {
        var response = await Client.PutAsJsonAsync($"users/{username}/decks/{deckId}", deck);
        return response.IsSuccessStatusCode;
    }
}