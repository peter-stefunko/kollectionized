namespace Kollectionized.Models;

public class PokemonCardFilter
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Typing { get; set; }
    public string? Form { get; set; }
    public string? Set { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}