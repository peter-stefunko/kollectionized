using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Helpers;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Kollectionized.Models;

public class PokemonCard
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Set { get; set; } = string.Empty;
    public string? Rarity { get; set; }
    public string Type { get; set; } = string.Empty;
    public int? PokedexNumber { get; set; }
    public string Typings { get; set; } = string.Empty;
    public string Form { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    
    [JsonIgnore]
    private Task<Bitmap?>? _imageTask;
    
    public Task<Bitmap?> GetImageAsync()
    {
        _imageTask ??= ImageHelper.LoadFromWeb(new Uri($"https://luvbckqisrgzkrsckfqp.supabase.co/storage/v1/object/public{ImageUrl}"));
        return _imageTask;
    }
}