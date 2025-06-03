using Kollectionized.Api.Data;
using Kollectionized.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kollectionized.Api.Controllers;

[ApiController]
[Route("api/users/{username}/decks")]
public class DecksController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PokemonDeck>>> GetUserDecks(string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return NotFound("User not found.");

        var decks = await context.PokemonDecks
            .Where(d => d.UserId == user.Id)
            .Include(d => d.CardInstances)
            .ThenInclude(i => i.Card)
            .ToListAsync();

        return Ok(decks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeck(string username, [FromBody] PokemonDeck deck)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return NotFound("User not found.");

        deck.UserId = user.Id;
        deck.CreatedAt = DateTime.UtcNow;

        context.PokemonDecks.Add(deck);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDeckById), new { username, deckId = deck.Id }, deck);
    }

    [HttpPost("{deckId:guid}/cards")]
    public async Task<IActionResult> AddCardToDeck(string username, Guid deckId, [FromBody] Guid instanceId)
    {
        var deck = await context.PokemonDecks
            .Include(d => d.CardInstances)
            .FirstOrDefaultAsync(d => d.Id == deckId);

        if (deck == null) return NotFound("Deck not found.");

        if (deck.CardInstances.Any(i => i.Id == instanceId))
            return BadRequest("Card instance already in deck.");

        var instance = await context.PokemonCardInstances.FirstOrDefaultAsync(i => i.Id == instanceId);
        if (instance == null) return NotFound("Card instance not found.");

        deck.CardInstances.Add(instance);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{deckId:guid}")]
    public async Task<IActionResult> DeleteDeck(string username, Guid deckId)
    {
        var deck = await context.PokemonDecks.FirstOrDefaultAsync(d => d.Id == deckId);
        if (deck == null) return NotFound("Deck not found.");

        context.PokemonDecks.Remove(deck);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{deckId:guid}/cards")]
    public async Task<IActionResult> RemoveCardFromDeck(string username, Guid deckId, [FromBody] Guid instanceId)
    {
        var deck = await context.PokemonDecks
            .Include(d => d.CardInstances)
            .FirstOrDefaultAsync(d => d.Id == deckId);

        if (deck == null) return NotFound("Deck not found.");

        var instance = deck.CardInstances.FirstOrDefault(i => i.Id == instanceId);
        if (instance != null)
        {
            deck.CardInstances.Remove(instance);
            await context.SaveChangesAsync();
        }

        return NoContent();
    }

    [HttpPut("{deckId:guid}")]
    public async Task<IActionResult> UpdateDeck(string username, Guid deckId, [FromBody] PokemonDeck updatedDeck)
    {
        var deck = await context.PokemonDecks.FirstOrDefaultAsync(d => d.Id == deckId);
        if (deck == null) return NotFound("Deck not found.");

        deck.Name = updatedDeck.Name;
        deck.Description = updatedDeck.Description;
        deck.IsPublic = updatedDeck.IsPublic;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{deckId:guid}")]
    public async Task<ActionResult<PokemonDeck>> GetDeckById(string username, Guid deckId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return NotFound("User not found.");

        var deck = await context.PokemonDecks
            .Where(d => d.UserId == user.Id && d.Id == deckId)
            .Include(d => d.User)
            .Include(d => d.CardInstances).ThenInclude(i => i.Card)
            .Include(d => d.CardInstances).ThenInclude(i => i.Owner)
            .FirstOrDefaultAsync();

        return deck == null ? NotFound("Deck not found.") : Ok(deck);
    }
}