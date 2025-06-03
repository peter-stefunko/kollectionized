using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Kollectionized.Helpers;

public static class ImageHelper
{
    private static readonly HttpClient HttpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(10)
    };

    public static Bitmap LoadFromResource(Uri resourceUri)
    {
        try
        {
            var stream = AssetLoader.Open(resourceUri);
            return new Bitmap(stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ImageHelper] Failed to load resource image: {ex.Message}");
            throw;
        }
    }

    public static async Task<Bitmap?> LoadFromWebAsync(Uri url)
    {
        try
        {
            var data = await HttpClient.GetByteArrayAsync(url);
            return new Bitmap(new MemoryStream(data));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ImageHelper] Failed to load image from {url}: {ex.Message}");
            return null;
        }
    }
}