namespace Kollectionized.Api.Dtos;

public class PokemonCardFilterDto
{
    public string? Name { get; set; } = null;
    public string? Type { get; set; } = null;
    public string? Typing { get; set; } = null;
    public string? Form { get; set; } = null;
    public string? Set { get; set; } = null;
    public int Limit { get; set; } = 30;
    public int Offset { get; set; } = 0;
}