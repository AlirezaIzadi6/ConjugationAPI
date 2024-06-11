using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.General;

namespace ToLearnApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnitsController : MyController
{
    private readonly ApplicationDbContext _context;

    public UnitsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Units/5
    [HttpGet("deck{id}")]
    public async Task<ActionResult<IEnumerable<UnitDto>>> Getunits(int id)
    {
        // Retrieve units with requested DeckId and return a list of DTOs.
        var units = await _context.units.Where(e => e.DeckId == id).ToListAsync();
        var unitDtos = new List<UnitDto>();

        foreach (var unit in units)
        {
            unitDtos.Add(unit.GetDto());
        }

        return Ok(unitDtos);
    }

    // GET: api/Units/10
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitDto>> GetUnit(int id)
    {
        // Find the unit with requested id and return its DTO.
        var unit = await _context.units.FindAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        return unit.GetDto();
    }

    // GET: api/units/10/getcards
    [HttpGet("{id}/getcards")]
    public async Task<ActionResult<IEnumerable<Card>>> GetCards(int id)
    {
        // Find cards that the requested unit includes, and return a list of DTOs.
        var unit = await _context.units.FindAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        var cards = await _context.cards.Where(e => e.UnitId  == id)
            .ToListAsync();
        var cardDtos = new List<CardDto>();

        foreach (var card in cards)
        {
            cardDtos.Add(card.GetDto());
        }

        return Ok(cardDtos);
    }

    // PUT: api/units/10/editcards
    [HttpPut("{id}/editcards")]
    [Authorize]
    public async Task<ActionResult> EditCards(int id, List<CardDto> cardDtos)
    {
        // Find requested unit and check for errors.
        var unit = await _context.units.FindAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        if (CurrentUser(User) != unit.UserId)
        {
            return Unauthorized();
        }

        // Get unit cards.
        var cards = await _context.cards.Where(e => e.UnitId == id)
            .ToListAsync();

        // For each posted DTO, first check if it's for this unit id. Then if any card with its id exists, update it with DTO. Otherwise, create a new card.
        foreach (var cardDto in cardDtos)
        {
            if (cardDto.UnitId != id)
            {
                continue;
            }

            var card = cards.Find(e =>  e.Id == cardDto.Id);
            if (card == null)
            {
                var newCard = cardDto.GetCard(CurrentUser(User));
                _context.cards.Add(newCard);
            }

            else
            {
                card.UpdateWithDto(cardDto);
                _context.Entry(unit).State = EntityState.Modified;
            }
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    // PUT: api/Units/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutUnit(int id, UnitDto unitDto)
    {
        // Find requested unit and check errors.
        var unit = _context.units.Find(id);

        if (unit == null)
        {
            return NotFound(id);
        }

        if (id != unit.Id)
        {
            return BadRequest(new Error("Wrong Id", "You are requesting a different id than the unit you are trying to modify."));
        }

        if (CurrentUser(User) != unit.UserId)
        {
            return Unauthorized();
        }

        // No error occurred, so Update unit.

        // Before updating unit, store required old information.
        string oldName = unit.Name;
        int oldOrderNumber = unit.OrderNumber;
        unit.UpdateWithDto(unitDto);

        // If changing order was unsuccessful, reset OrderNumber to previous value.
        bool changeOrder = await ChangeOrder(oldOrderNumber, unit.OrderNumber);
        if (!changeOrder)
        {
            unit.OrderNumber = oldOrderNumber;
        }

        // If new name exists in the deck, return duplicate error.
        if (unit.Name != oldName && !IsUnique(unit))
        {
            return BadRequest(new Error("Duplicate name", "This name already exists."));
        }

        _context.Entry(unit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UnitExists(id))
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

    // POST: api/Units
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<UnitDto>> PostUnit(UnitDto unitDto)
    {
        var unit = unitDto.GetUnit(CurrentUser(User));

        // Find requested deck and check errors.
        var deck = await _context.decks.FindAsync(unitDto.DeckId);

        if (deck == null)
        {
            return BadRequest(unitDto);
        }

        if (CurrentUser(User) != deck.Creator)
        {
            return Unauthorized();
        }

        if (!IsUnique(unit))
        {
            return BadRequest(new Error("Duplicate name", "This name already exists."));
        }

        // Define OrderNumber and save card.
        int previousUnitsCount = await _context.units.CountAsync(e => e.DeckId  == unit.DeckId);
        unit.OrderNumber = previousUnitsCount+1;
        _context.units.Add(unit);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUnit", new { id = unit.Id }, unit.GetDto());
    }

    // DELETE: api/Units/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUnit(int id)
    {
        // Find requested card and check for errors.
        var unit = await _context.units.FindAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        if (CurrentUser(User) != unit.UserId)
        {
            return Unauthorized();
        }

        // Delete and save.
        _context.units.Remove(unit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Get current and requested OrderNumber as input and return true if changes were successful.
    // OrderNumber starts from 1, and 0 for target means removing.
    private async Task<bool> ChangeOrder(int start, int target)
    {
        if (target == start)
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
            // Find all next units and decrease OrderNumber 1 step.
            var units = await _context.units.Where(e => e.OrderNumber > target)
                .ToListAsync();

            foreach (var unit in units)
            {
                unit.OrderNumber--;
                _context.Entry(unit).State = EntityState.Modified;
            }
        }

        // Move card down (OrderNumber increases).
        else if (target > start)
        {
            // Find requested unit and if it exists, change OrderNumber to target value.
            var targetUnit = await _context.units.FirstOrDefaultAsync(e => e.OrderNumber == start);

            if (targetUnit == null)
            {
                return false;
            }

            targetUnit.OrderNumber = target;

            // Find units with OrderNumber between start and target, and move them up all except requested unit.
            var units = await _context.units.Where(e => e.OrderNumber > start && e.OrderNumber <= target)
                .ToListAsync();

            foreach (var unit in units)
            {
                if (unit.Id == targetUnit.Id)
                {
                    continue;
                }
                unit.OrderNumber--;
                _context.Entry(unit).State = EntityState.Modified;
            }
        }

        else if (target < start)
        {
            var targetUnit = await _context.units.FirstOrDefaultAsync(e => e.OrderNumber == start);
            if (targetUnit == null)
            {
                return false;
            }
            targetUnit.OrderNumber = target;

            var units = await _context.units.Where(e => e.OrderNumber >= target && e.OrderNumber < start).ToListAsync();
            foreach (var unit in units)
            {
                if (unit.Id == targetUnit.Id)
                {
                    continue;
                }
                unit.OrderNumber++;
                _context.Entry(unit).State = EntityState.Modified;
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

    private bool UnitExists(int id)
    {
        return _context.units.Any(e => e.Id == id);
    }

    private bool IsUnique(Unit unit)
    {
        // One deck cannot have two units with the same name.
        return !_context.units.Any(e => e.Name == unit.Name && e.DeckId == unit.DeckId);
    }
}
