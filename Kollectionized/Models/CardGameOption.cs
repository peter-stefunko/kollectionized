using CommunityToolkit.Mvvm.Input;
using System;
using Avalonia.Media.Imaging;
using Kollectionized.Helpers;

namespace Kollectionized.Models;

public class CardGameOption(string gameKey, string name, string assetPath, Action openAction)
{
    public string GameKey { get; } = gameKey;
    public string Name { get; } = name;
    public Bitmap? Image { get; } = ImageHelper.LoadFromResource(new Uri(assetPath));
    public IRelayCommand OpenCommand { get; } = new RelayCommand(openAction);
}