using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Flashcards;

namespace ToLearnApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DecksController : MyController
{
    private readonly ConjugationContext _context;

    public DecksController(ConjugationContext context)
    {
        _context = context;
    }

    // GET: api/Decks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Deck>>> Getdecks()
    {
        return await _context.decks.ToListAsync();
    }

    // GET: api/Decks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Deck>> GetDeck(int id)
    {
        var deck = await _context.decks.FindAsync(id);

        if (deck == null)
        {
            return NotFound();
        }

        return deck;
    }

    // PUT: api/Decks/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeck(int id, Deck deck)
    {
        if (id != deck.Id)
        {
            return BadRequest();
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
    public async Task<ActionResult<Deck>> PostDeck(Deck deck)
    {
        _context.decks.Add(deck);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDeck", new { id = deck.Id }, deck);
    }

    // DELETE: api/Decks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeck(int id)
    {
        var deck = await _context.decks.FindAsync(id);
        if (deck == null)
        {
            return NotFound();
        }

        _context.decks.Remove(deck);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DeckExists(int id)
    {
        return _context.decks.Any(e => e.Id == id);
    }
}
