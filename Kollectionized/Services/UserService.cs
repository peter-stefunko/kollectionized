using System;
using System.Collections.Generic;
using Kollectionized.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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
        var body = new
        {
            username,
            password
        };
        
        var response = await _client.PostAsJsonAsync("auth/register", body);
        return response.IsSuccessStatusCode
            ? null
            : await response.Content.ReadAsStringAsync();
    }

    public async Task<User?> Login(string username, string password)
    {
        var body = new
        {
            username,
            password
        };
        
        var response = await _client.PostAsJsonAsync("auth/login", body);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        return new User { Username = username, Id = result!.UserId };
    }

    public async Task<string?> DeleteAccount(string username, string password)
    {
        var body = new
        {
            username,
            password
        };
        
        var request = new HttpRequestMessage(HttpMethod.Delete, "auth/user")
        {
            Content = JsonContent.Create(body)
        };

        var response = await _client.SendAsync(request);
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }
    
    public async Task<string?> ChangeUsername(string currentUsername, string password, string newUsername)
    {
        var body = new
        {
            currentUsername,
            password,
            newUsername
        };

        var response = await _client.PutAsJsonAsync("auth/change-username", body);
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        var response = await _client.GetAsync("auth/users");
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) return [];

        var result = JsonSerializer.Deserialize<List<User>>(json);
        return result ?? [];
    }

    
    private class LoginResponse
    {
        public Guid UserId { get; init; }
    }
}