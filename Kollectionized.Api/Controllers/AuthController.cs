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
        catch (Exception ex)
        {
            Console.WriteLine($"Register failed: {ex.Message}");
            return StatusCode(500, "Something went wrong on the server.");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null)
        {
            return Unauthorized("User not found.");
        }
        
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

        return Ok(new { userId = user.Id });
    }

    [HttpDelete("user")]
    public async Task<IActionResult> DeleteAccount([FromBody] AccountDeleteDto dto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        
            if (user == null)
                return Unauthorized("User not found.");
        
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid password.");
        
            if (user.Username.StartsWith("[del-"))
                return BadRequest("Account is already deleted.");

            var userId = user.Id;
        
            user.LastUsername = user.Username;
            user.Username = $"[del-{userId}]";
            user.PasswordHash = null;
            context.Users.Update(user);
        
            var collections = await context.PokemonCollections
                .Where(c => c.UserId == userId)
                .ToListAsync();
            context.PokemonCollections.RemoveRange(collections);
        
            var decks = await context.PokemonDecks
                .Where(d => d.UserId == userId)
                .ToListAsync();
            context.PokemonDecks.RemoveRange(decks);

            await context.SaveChangesAsync();
            return Ok(new { message = "Account deleted (soft)." });   
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Action failed: {ex.Message}");
            return StatusCode(500, "Something went wrong on the server.");
        }
    }
    
    [HttpPut("change-username")]
    public async Task<IActionResult> ChangeUsername([FromBody] UsernameChangeDto dto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.CurrentUsername);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password.");

            if (!UsernameValidation.IsValid(dto.NewUsername, out var error))
                return BadRequest(error);

            var exists = await context.Users.AnyAsync(u => u.Username == dto.NewUsername && u.Id != user.Id);
            if (exists)
                return BadRequest("That username is already taken.");

            user.Username = dto.NewUsername;
            context.Users.Update(user);
            await context.SaveChangesAsync();

            return Ok(new { message = "Name changed successfully." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Action failed: {ex.Message}");
            return StatusCode(500, "Something went wrong on the server.");
        }
    }

}