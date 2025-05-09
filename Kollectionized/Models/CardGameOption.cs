namespace Kollectionized.Models;

public class CardGameOption(string gameKey, string name)
{
    public string GameKey { get; } = gameKey;
    public string Name { get; } = name;
}