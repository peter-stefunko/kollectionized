namespace Kollectionized.Api.Dtos;

public class CardInstanceDeleteDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public List<Guid> InstanceIds { get; set; } = [];
}