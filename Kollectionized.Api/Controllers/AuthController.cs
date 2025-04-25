using Microsoft.AspNetCore.Mvc;
using Kollectionized.Api.Data;
using Kollectionized.Api.Models;
using Kollectionized.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Kollectionized.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        try
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Username is already taken.");

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = hash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Register failed: {ex.Message}");
            return StatusCode(500, "Something went wrong on the server.");
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

        return Ok(new { userId = user.Id });
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteAccount(string username, [FromQuery] string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return Unauthorized("Invalid password.");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Account deleted." });
    }
}