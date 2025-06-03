using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kollectionized.Api.Data;
using Kollectionized.Api.Dtos;

namespace Kollectionized.Api.Controllers;

[ApiController]
[Route("api/users")]
public class AccountUpdateController(AppDbContext context) : ControllerBase
{
    [HttpPut("{username}")]
    public async Task<IActionResult> UpdateAccount(string username, [FromBody] AccountUpdateDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid password.");

        if (!string.IsNullOrWhiteSpace(dto.NewUsername) && dto.NewUsername != username)
        {
            var exists = await context.Users.AnyAsync(u => u.Username == dto.NewUsername);
            if (exists) return BadRequest("Username already taken.");

            user.Username = dto.NewUsername;
        }

        user.Bio = dto.Bio;
        context.Users.Update(user);
        await context.SaveChangesAsync();

        return Ok(new { message = "Account updated." });
    }

    [HttpPut("{username}/password")]
    public async Task<IActionResult> ChangePassword(string username, [FromBody] PasswordChangeDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
            return Unauthorized("Invalid password.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        context.Users.Update(user);
        await context.SaveChangesAsync();

        return Ok(new { message = "Password changed." });
    }
}