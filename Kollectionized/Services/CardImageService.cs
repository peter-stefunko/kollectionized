using System;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Kollectionized.Models;
using Kollectionized.Helpers;
using Kollectionized.Common;

namespace Kollectionized.Services;

public static class CardImageService
{
    public static Task<Bitmap?> LoadCardImageAsync(PokemonCard card)
    {
        var url = new Uri($"{Constants.Storage.SupabaseBaseUrl}{card.ImageUrl}");
        return ImageHelper.LoadFromWebAsync(url);
    }
}