namespace Kollectionized.Models;

public class PokemonCardFilter
{
    public string? Name { get; set; }
    public string? CardType { get; set; }
    public string? Type { get; set; }
    public string? SubType { get; set; }
    public string? Set { get; set; }
    public int? Limit { get; set; } = 310;
    public int? Offset { get; set; } = 0;
}