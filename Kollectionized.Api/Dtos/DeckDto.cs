using Kollectionized.Api.Models;

namespace Kollectionized.Api.Dtos;

public record DeckDto(
    Guid Id,
    string Name,
    string Description,
    bool IsPublic,
    DateTime CreatedAt,
    Guid UserId,
    string Username,
    List<CardInstance> CardInstances
);
