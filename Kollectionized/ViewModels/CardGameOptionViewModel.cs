using System;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using Kollectionized.Helpers;
using Kollectionized.Views;

namespace Kollectionized.ViewModels;

public class CardGameOptionViewModel : ViewModelBase
{
    public string GameKey { get; }
    public string Name { get; }
    public Bitmap Image { get; }
    public IRelayCommand OpenCommand { get; }

    public CardGameOptionViewModel(string gameKey, string name, string assetPath)
    {
        GameKey = gameKey;
        Name = name;
        Image = ImageHelper.LoadFromResource(new Uri(assetPath));
        OpenCommand = new RelayCommand(() => new AllCardsWindow(GameKey).Show());
    }
}