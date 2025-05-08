using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Kollectionized.Api.Dtos;

public class PokemonCardFilterDto
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Typing { get; set; }
    public string? Form { get; set; }
    public string? Set { get; set; }
    public int Limit { get; set; } = 310;
    public int Offset { get; set; } = 0;
}