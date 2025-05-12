using Kollectionized.Api.Data;
using Kollectionized.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kollectionized.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        try
        {
            var users = await context.Users
                .Where(u => !u.Username.StartsWith("[del-"))
                .OrderBy(u => u.Username)
                .Select(u => new UserDto(u.Id, u.Username, u.CreatedAt, u.LastUsername, u.Bio))
                .ToListAsync();

            return Ok(users);
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserByUsername(Guid id)
    {
        try
        {
            var user = await context.Users
                .Where(u => u.Id == id && !u.Username.StartsWith("[del-"))
                .Select(u => new UserDto(u.Id, u.Username, u.CreatedAt, u.LastUsername, u.Bio))
                .FirstOrDefaultAsync();

            return user is null ? NotFound("User not found.") : Ok(user);
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }
}
