namespace Kollectionized.Api.Dtos;

public record UserDto(Guid Id, string Username, DateTimeOffset CreatedAt, string LastUsername, string Bio);