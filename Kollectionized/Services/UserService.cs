using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Kollectionized.Models;

namespace Kollectionized.Services;

public class UserService : ServiceBase
{
    public async Task<string?> Register(string username, string password)
    {
        var response = await Client.PostAsJsonAsync("auth/register", new { username, password });
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }

    public async Task<User?> Login(string username, string password)
    {
        var response = await Client.PostAsJsonAsync("auth/login", new { username, password });
        if (!response.IsSuccessStatusCode) return null;

        var user = await response.Content.ReadFromJsonAsync<User>();
        return user;
    }

    public async Task<string?> DeleteAccount(string username, string password)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, "auth/user")
        {
            Content = JsonContent.Create(new { username, password })
        };
        var response = await Client.SendAsync(request);
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> UpdateAccount(string username, string password, string? newUsername, string? bio)
    {
        var payload = new { password, newUsername, bio };
        var response = await Client.PutAsJsonAsync($"users/{username}", payload);
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }

    public async Task<string?> ChangePassword(string username, string currentPassword, string newPassword)
    {
        var payload = new { currentPassword, newPassword };
        var response = await Client.PutAsJsonAsync($"users/{username}/password", payload);
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        var response = await Client.GetAsync("users");
        if (!response.IsSuccessStatusCode) return [];

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<User>>(json) ?? [];
    }
    
    private class LoginResponse
    {
        public Guid UserId { get; init; }
        public string Username { get; init; } = string.Empty;
        public string Bio { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public string LastUsername { get; init; } = string.Empty;
    }
}