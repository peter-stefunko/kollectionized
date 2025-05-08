using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Kollectionized.Helpers;

namespace Kollectionized.Models;

public class PokemonCard
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string SetName { get; set; } = string.Empty;
    public string Rarity { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    
    [JsonIgnore]
    private Task<Bitmap?>? _imageTask;
    
    public Task<Bitmap?> GetImageAsync()
    {
        _imageTask ??= ImageHelper.LoadFromWeb(new Uri($"https://luvbckqisrgzkrsckfqp.supabase.co/storage/v1/object/public{ImageUrl}"));
        return _imageTask;
    }

}