using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.General;

namespace ToLearnApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardsController : MyController
{
    private readonly ApplicationDbContext _context;

    public CardsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Cards
    [HttpGet("unit{id}")]
    public async Task<ActionResult<IEnumerable<CardDto>>> Getcards(int id)
    {
        var cards = await _context.cards.Where(e => e.UnitId == id).ToListAsync();
        var cardDtos = new List<CardDto>();
        foreach (var card in cards)
        {
            cardDtos.Add(card.GetDto());
        }
        return Ok(cardDtos);
    }

    // GET: api/Cards/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CardDto>> GetCard(int id)
    {
        var card = await _context.cards.FindAsync(id);

        if (card == null)
        {
            return NotFound();
        }

        return card.GetDto();
    }

    // PUT: api/Cards/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutCard(int id, CardDto cardDto)
    {
        var card = _context.cards.Find(id);
        if (card == null)
        {
            return NotFound(id);
        }

        if (id != card.Id)
        {
            return BadRequest(new Error("Wrong Id", "You are requesting a different id than the card you are trying to modify."));
        }

        if (CurrentUser(User) != card.Creator)
        {
            return Unauthorized();
        }

        int oldOrderNumber = card.OrderNumber;
        card.UpdateWithDto(cardDto);
        bool changeOrder = await ChangeOrder(oldOrderNumber, card.OrderNumber);
        if (!changeOrder)
        {
            card.OrderNumber = oldOrderNumber;
        }

        _context.Entry(card).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CardExists(id))
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

    // POST: api/Cards
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CardDto>> PostCard(CardDto cardDto)
    {
        var card = cardDto.GetCard(CurrentUser(User));
        var unit = await _context.units.FindAsync(cardDto.UnitId);
        if (unit == null)
        {
            return BadRequest(new Error("Wrong unit Id", "Unit with your requested Id has not found."));
        }

        if (CurrentUser(User) != unit.Creator)
        {
            return Unauthorized();
        }

        var previousCardsCount = await _context.cards.CountAsync(e => e.UnitId  == card.UnitId);
        card.OrderNumber = previousCardsCount+1;
        _context.cards.Add(card);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCard", new { id = card.Id }, card.GetDto());
    }

    // DELETE: api/Cards/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCard(int id)
    {
        var card = await _context.cards.FindAsync(id);
        if (card == null)
        {
            return NotFound();
        }

        if (CurrentUser(User) != card.Creator)
        {
            return Unauthorized();
        }

        _context.cards.Remove(card);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CardExists(int id)
    {
        return _context.cards.Any(e => e.Id == id);
    }

    private async Task<bool> ChangeOrder(int start, int target)
    {
        if (target== start)
        {
            return true;
        }

        if (target < 0 || start <= 0)
        {
            return false;
        }

        if (target == 0)
        {
            var cards = await _context.cards.Where(e => e.OrderNumber > target).ToListAsync();
            foreach (var card in cards)
            {
                card.OrderNumber--;
                _context.Entry(card).State = EntityState.Modified;
            }
        }

        else if(target > start)
        {
            var targetCard = await _context.cards.FirstOrDefaultAsync(e => e.OrderNumber == start);
            if (targetCard == null)
            {
                return false;
            }
            targetCard.OrderNumber = target;

            var cards = await _context.cards.Where(e => e.OrderNumber > start && e.OrderNumber <= target).ToListAsync();
            foreach (var card in cards)
            {
                if (card.Id == targetCard.Id)
                {
                    continue;
                }
                card.OrderNumber--;
                _context.Entry(card).State = EntityState.Modified;
            }
        }

        else if (target < start)
        {
            var targetCard = await _context.cards.FirstOrDefaultAsync(e => e.OrderNumber == start);
            if (targetCard == null)
            {
                return false;
            }
            targetCard.OrderNumber = target;

            var cards = await _context.cards.Where(e => e.OrderNumber >= target && e.OrderNumber < start).ToListAsync();
            foreach (var card in cards)
            {
                if (card.Id == targetCard.Id)
                {
                    continue;
                }
                card.OrderNumber++;
                _context.Entry(card).State = EntityState.Modified;
            }
        }
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
