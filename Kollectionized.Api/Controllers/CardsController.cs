using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Kollectionized.Api.Data;
using Kollectionized.Api.Dtos;
using Kollectionized.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kollectionized.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController(AppDbContext context) : ControllerBase
{
    [HttpPost("pokemon")]
    public async Task<ActionResult<IEnumerable<PokemonCard>>> GetPokemonCards(PokemonCardFilterDto dto)
    {
        try
        {
            var query = context.PokemonCards.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Name))
                query = query.Where(c => c.Name.ToLower().Contains(dto.Name.ToLower()));

            if (!string.IsNullOrWhiteSpace(dto.Type))
                query = query.Where(c => c.Type == dto.Type);

            if (!string.IsNullOrWhiteSpace(dto.Typing))
            {
                var serializedTyping = JsonSerializer.Serialize(new[] { dto.Typing });

                query = query.Where(c =>
                    c.Typings != null && EF.Functions.JsonContains(c.Typings, serializedTyping));
            }

            if (!string.IsNullOrWhiteSpace(dto.Form))
            {
                var serializedForm = JsonSerializer.Serialize(new[] { dto.Form });

                query = query.Where(c =>
                    EF.Functions.JsonContains(c.Forms, serializedForm));
            }

            if (!string.IsNullOrWhiteSpace(dto.Set))
                query = query.Where(c => c.Set == dto.Set);

            var cards = await query
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Set)
                .Skip(dto.Offset)
                .Take(dto.Limit)
                .ToListAsync();

            return Ok(cards);
        }
        catch
        {
            return StatusCode(500, "Something went wrong on the server");
        }
    }
}