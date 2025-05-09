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

    public async Task<string?> ChangeUsername(string currentUsername, string password, string newUsername)
    {
        var response = await Client.PutAsJsonAsync("auth/change-username",
            new { currentUsername, password, newUsername });
        return response.IsSuccessStatusCode ? null : await response.Content.ReadAsStringAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        var response = await Client.GetAsync("users");
        if (!response.IsSuccessStatusCode) return [];

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<User>>(json) ?? [];
    }
}