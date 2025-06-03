using System.Net.Http.Json;
using System.Threading.Tasks;
using Kollectionized.Models;

namespace Kollectionized.Services;

public class SetsService : ServiceBase
{
    public async Task<PokemonSet?> GetSetByNameAsync(string name)
    {
        var response = await Client.GetAsync($"sets/{name}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PokemonSet>();
    }
}
