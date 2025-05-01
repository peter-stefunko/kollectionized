using System;
using Kollectionized.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Kollectionized.Services;

public class UserService
{
    private readonly HttpClient _client;

    public UserService()
    {
        var baseUrl = Environment.GetEnvironmentVariable("KOLLECTIONIZED_API")
                      ?? "https://kollectionized-api-delicate-field-1881.fly.dev/api/";
        _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public async Task<string?> Register(string username, string password)
    {
        var response = await _client.PostAsJsonAsync("auth/register", new { username, password });
        return response.IsSuccessStatusCode
            ? null
            : await response.Content.ReadAsStringAsync();
    }

    public async Task<User?> Login(string username, string password)
    {
        var response = await _client.PostAsJsonAsync("auth/login", new { username, password });
        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        return new User { Username = username, Id = result!.UserId };
    }

    public async Task<bool> DeleteAccount(string username, string password)
    {
        var url = $"auth/{username}?password={Uri.EscapeDataString(password)}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ChangeUsername(Guid userId, string newUsername)
    {
        var response = await _client.PutAsJsonAsync($"user/{userId}/username", new { username = newUsername });
        return response.IsSuccessStatusCode;
    }

    private class LoginResponse
    {
        public Guid UserId { get; set; }
    }
}