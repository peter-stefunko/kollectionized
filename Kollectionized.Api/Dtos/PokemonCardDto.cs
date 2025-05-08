namespace Kollectionized.Api.Dtos;

public record PokemonCardDto(Guid Uuid, string Set, string CardNumber, string Name, string Type, string Form, string ImageUrl, string? Rarity, int? PokedexNumber, string? Typings);