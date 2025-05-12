using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Kollectionized.Models;

namespace Kollectionized.Services;

public class UserCardService : ServiceBase
{
    public async Task<List<CardInstance>> GetUserCardInstances(string username)
    {
        try
        {
            var response = await Client.GetAsync($"users/{username}/cards");
            if (!response.IsSuccessStatusCode) return [];

            var instances = await response.Content.ReadFromJsonAsync<List<CardInstance>>();
            return instances ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to fetch card instances for {username}: {ex.Message}");
            return [];
        }
    }

    public async Task<string?> AddCardInstance(Guid cardId, double? grade, string? company, string notes)
    {
        var user = AuthService.CurrentUser;
        var password = AuthService.CurrentPassword;
        if (user == null || string.IsNullOrWhiteSpace(password)) return "User not authenticated.";

        var payload = new
        {
            cardId,
            grade,
            gradingCompany = company,
            notes,
            password
        };

        var response = await Client.PostAsJsonAsync($"users/{user.Username}/cards", payload);
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> UpdateCardInstance(Guid instanceId, double? grade, string? company, string notes)
    {
        var user = AuthService.CurrentUser;
        var password = AuthService.CurrentPassword;
        if (user == null || string.IsNullOrWhiteSpace(password)) return "User not authenticated.";

        var payload = new
        {
            grade,
            gradingCompany = company,
            notes,
            password
        };

        var response = await Client.PutAsJsonAsync($"users/{user.Username}/cards/{instanceId}", payload);
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> DeleteCardInstance(Guid instanceId)
    {
        var user = AuthService.CurrentUser;
        var password = AuthService.CurrentPassword;
        if (user == null || string.IsNullOrWhiteSpace(password)) return "User not authenticated.";

        var payload = new { password };
        
        var request = new HttpRequestMessage(HttpMethod.Delete, $"users/{user.Username}/cards/{instanceId}")
        {
            Content = JsonContent.Create(payload)
        };

        var response = await Client.SendAsync(request);
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }
}