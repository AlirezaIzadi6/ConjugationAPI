using Microsoft.AspNetCore.Authorization;
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

    // GET: api/Cards/10
    [HttpGet("unit{id}")]
    public async Task<ActionResult<IEnumerable<CardDto>>> Getcards(int id)
    {
        // Retrieve cards with requested UnitId and return a list of DTOs.
        var cards = await _context.cards.Where(e => e.UnitId == id)
            .ToListAsync();
        var cardDtos = new List<CardDto>();

        foreach (var card in cards)
        {
            cardDtos.Add(card.GetDto());
        }

        return cardDtos;
    }

    // GET: api/Cards/50
    [HttpGet("{id}")]
    public async Task<ActionResult<CardDto>> GetCard(int id)
    {
        // Find the card with requested id and return its DTO.
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
        // Find requested card and check errors.
        var card = await _context.cards.FindAsync(id);

        if (card == null)
        {
            return NotFound();
        }

        if (cardDto.Id != card.Id)
        {
            return BadRequest(new Error("Wrong Id", "You are requesting a different id than the card you are trying to modify."));
        }

        if (CurrentUser(User) != card.UserId)
        {
            return Unauthorized();
        }

        // No error occurred, so Update card.

        int oldOrderNumber = card.OrderNumber; // Store this value before updating card.
        card.UpdateWithDto(cardDto);
        // If changing order was unsuccessful, reset OrderNumber to previous value.
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

        // Find requested unit and check errors.
        var unit = await _context.units.FindAsync(cardDto.UnitId);

        if (unit == null)
        {
            return BadRequest(new Error("Wrong unit Id", "Unit with your requested Id has not found."));
        }

        if (CurrentUser(User) != unit.UserId)
        {
            return Unauthorized();
        }

        // Define OrderNumber and save card.
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
        // Find requested card and check for errors.
        var card = await _context.cards.FindAsync(id);

        if (card == null)
        {
            return NotFound();
        }

        if (CurrentUser(User) != card.UserId)
        {
            return Unauthorized();
        }

        // Delete and save.
        _context.cards.Remove(card);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Get current and requested OrderNumber as input and return true if changes were successful.
    // OrderNumber starts from 1, and 0 for target means removing.
    private async Task<bool> ChangeOrder(int start, int target)
    {
        if (target== start)
        {
            return true; // Actually no change is needed to process this.
        }

        // Negative values are not accepted.
        if (target < 0 || start <= 0)
        {
            return false;
        }

        // Remove card.
        if (target == 0)
        {
            // Find all next cards and decrease OrderNumber 1 step.
            var cards = await _context.cards.Where(e => e.OrderNumber > target)
                .ToListAsync();

            foreach (var card in cards)
            {
                card.OrderNumber--;
                _context.Entry(card).State = EntityState.Modified;
            }
        }

        // Move card down (OrderNumber increases).
        else if(target > start)
        {
            // Find requested card and if it exists, change OrderNumber to target value.
            var targetCard = await _context.cards.FirstOrDefaultAsync(e => e.OrderNumber == start);

            if (targetCard == null)
            {
                return false;
            }

            targetCard.OrderNumber = target;

            // Find cards with OrderNumber between start and target, and move them up all except requested card.
            var cards = await _context.cards.Where(e => e.OrderNumber > start && e.OrderNumber <= target)
                .ToListAsync();

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

        // Try save changes or return false.
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

    private bool CardExists(int id)
    {
        return _context.cards.Any(e => e.Id == id);
    }
}
