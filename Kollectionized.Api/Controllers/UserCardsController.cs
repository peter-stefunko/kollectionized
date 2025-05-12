using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kollectionized.Api.Data;
using Kollectionized.Api.Dtos;
using Kollectionized.Api.Models;

namespace Kollectionized.Api.Controllers;

[ApiController]
[Route("api/users/{username}/cards")]
public class UserCardController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserCardInstances(string username)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return NotFound("User not found.");

            var instances = await context.PokemonCardInstances
                .Include(i => i.Card)
                .Include(i => i.Owner)
                .Where(i => i.CurrentOwner == user.Id)
                .ToListAsync();

            return Ok(instances);
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCardInstance(string username, [FromBody] CardInstanceCreateDto dto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var instance = new CardInstance
            {
                CardId = dto.CardId,
                CurrentOwner = user.Id,
                Grade = dto.Grade,
                GradingCompany = dto.GradingCompany ?? string.Empty,
                Notes = dto.Notes ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            context.PokemonCardInstances.Add(instance);
            await context.SaveChangesAsync();

            return Ok(new { message = "Card instance added." });
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCardInstance(string username, Guid id, [FromBody] CardInstanceUpdateDto dto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var instance =
                await context.PokemonCardInstances.FirstOrDefaultAsync(i => i.Id == id && i.CurrentOwner == user.Id);
            if (instance == null) return NotFound("Card instance not found.");

            instance.Grade = dto.Grade;
            instance.GradingCompany = dto.GradingCompany ?? string.Empty;
            instance.Notes = dto.Notes ?? string.Empty;

            await context.SaveChangesAsync();
            return Ok(new { message = "Card instance updated." });
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardInstance(string username, Guid id, [FromBody] PasswordOnlyDto dto)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var instance =
                await context.PokemonCardInstances.FirstOrDefaultAsync(i => i.Id == id && i.CurrentOwner == user.Id);
            if (instance == null) return NotFound("Card instance not found.");

            context.PokemonCardInstances.Remove(instance);
            await context.SaveChangesAsync();
            return Ok(new { message = "Card instance deleted." });
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }
}
