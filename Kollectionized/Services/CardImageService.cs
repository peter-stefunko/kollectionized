using System;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Kollectionized.Models;
using Kollectionized.Helpers;
using Kollectionized.Common;

namespace Kollectionized.Services;

public class CardImageService
{
    public async Task<Bitmap?> LoadCardImageAsync(PokemonCard card)
    {
        if (string.IsNullOrWhiteSpace(card.ImageUrl))
            return null;

        var url = new Uri($"{Constants.Storage.SupabaseBaseUrl}{card.ImageUrl}");
        return await ImageHelper.LoadFromWebAsync(url);
    }
}