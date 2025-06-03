using Kollectionized.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kollectionized.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetsController(AppDbContext context) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetSet(string name)
    {
        var set = await context.PokemonSets.FirstOrDefaultAsync(s => s.Name == name);
        return set is null ? NotFound("Set not found.") : Ok(set);
    }
}
