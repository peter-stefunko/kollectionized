using Microsoft.AspNetCore.Mvc;
using Kollectionized.Api.Data;
using Kollectionized.Api.Models;
using Kollectionized.Api.Dtos;
using Kollectionized.Api.Validation;
using Microsoft.EntityFrameworkCore;

namespace Kollectionized.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AppDbContext context) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        try
        {
            if (!UsernameValidation.IsValid(dto.Username, out var error))
                return BadRequest(error);

            if (await context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Username is already taken.");

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = hash
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully." });
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                CreatedAt = user.CreatedAt,
                LastUsername = user.LastUsername ?? string.Empty,
                Bio = user.Bio ?? string.Empty,
            };

            return Ok(userDto);
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }

    [HttpDelete("user/{username}")]
    public async Task<IActionResult> DeleteAccount(string username, PasswordOnlyDto dto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid password.");

            if (user.Username.StartsWith("[del-"))
                return BadRequest("Account is already deleted.");

            var userId = user.Id;

            user.LastUsername = user.Username;
            user.Username = $"[del-{userId}]";
            user.PasswordHash = null;
            context.Users.Update(user);

            var collections = await context.PokemonCollections.Where(c => c.UserId == userId).ToListAsync();
            context.PokemonCollections.RemoveRange(collections);

            var decks = await context.PokemonDecks.Where(d => d.UserId == userId).ToListAsync();
            context.PokemonDecks.RemoveRange(decks);

            await context.SaveChangesAsync();
            return Ok(new { message = "Account deleted (soft)." });
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }
}