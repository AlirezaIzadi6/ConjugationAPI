using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.General;

namespace ToLearnApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DecksController : MyController
{
    private readonly ApplicationDbContext _context;

    public DecksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Decks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeckDto>>> Getdecks()
    {
        var decks = await _context.decks.ToListAsync();
        List<DeckDto> deckDtos = new List<DeckDto>();
        foreach (var deck in decks)
        {
            deckDtos.Add(deck.GetDto());
        }
        return Ok(deckDtos);
    }

    // GET: api/Decks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DeckDto>> GetDeck(int id)
    {
        var deck = await _context.decks.FindAsync(id);

        if (deck == null)
        {
            return NotFound();
        }

        return deck.GetDto();
    }

    // PUT: api/Decks/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutDeck(int id, DeckDto deckDto)
    {
        var deck = _context.decks.Find(deckDto.Id);
        if (deck == null)
        {
            return NotFound(id);
        }
        if (id != deck.Id)
        {
            return BadRequest(new Error("Wrong Id", "You are requesting a different id than the deck you are trying to modify."));
        }

        if (CurrentUser(User) != deck.Creator)
        {
            return Unauthorized();
        }

        deck.UpdateWithDto(deckDto);
        if (!IsUnique(deck))
        {
            return BadRequest(new Error("Duplicate name", "This name already exists in your created decks"));
        }

        _context.Entry(deck).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DeckExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Decks
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<DeckDto>> PostDeck(DeckDto deckDto)
    {
        Deck deck = deckDto.GetDeck(CurrentUser(User));
        if (!IsUnique(deck))
        {
            return BadRequest(new Error("Duplicate name", "This name already exists in your created decks."));
        }
        _context.decks.Add(deck);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDeck", new { id = deck.Id }, deck.GetDto());
    }

    // DELETE: api/Decks/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteDeck(int id)
    {
        var deck = await _context.decks.FindAsync(id);
        if (deck == null)
        {
            return NotFound();
        }

        if (CurrentUser(User) != deck.Creator)
        {
            return Unauthorized();
        }

        _context.decks.Remove(deck);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DeckExists(int id)
    {
        return _context.decks.Any(e => e.Id == id);
    }

    private bool IsUnique(Deck deck)
    {
        return !_context.decks.Any(e => e.Creator == deck.Creator && e.Title == deck.Title);
    }
}
