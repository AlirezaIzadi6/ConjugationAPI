using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
public class UnitsController : MyController
{
    private readonly ConjugationContext _context;

    public UnitsController(ConjugationContext context)
    {
        _context = context;
    }

    // GET: api/Units
    [HttpGet("deck{id}")]
    public async Task<ActionResult<IEnumerable<UnitDto>>> Getunits(int id)
    {
        var units = await _context.units.Where(e => e.DeckId == id).ToListAsync();
        List<UnitDto> unitDtos = new List<UnitDto>();
        foreach (var unit in units)
        {
            unitDtos.Add(unit.GetDto());
        }
        return Ok(unitDtos);
    }

    // GET: api/Units/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitDto>> GetUnit(int id)
    {
        var unit = await _context.units.FindAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        return unit.GetDto();
    }

    [HttpGet("{id}/getcards")]
    public async Task<ActionResult<IEnumerable<Card>>> GetCards(int id)
    {
        var unit = await _context.units.FindAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        var cards = await _context.cards.Where(e => e.UnitId  == id).ToListAsync();
        var cardDtos = new List<CardDto>();
        foreach (var card in cards)
        {
            cardDtos.Add(card.GetDto());
        }
        return Ok(cardDtos);
    }

    [HttpPut("{id}/editcards")]
    [Authorize]
    public async Task<ActionResult> EditCards(int id, List<CardDto> cardDtos)
    {
        var unit = await _context.units.FindAsync(id);
        if (unit == null)
        {
            return NotFound();
        }

        if (CurrentUser(User) != unit.Creator)
        {
            return Unauthorized();
        }

        var cards = await _context.cards.Where(e => e.UnitId == id).ToListAsync();
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
        var unit = _context.units.Find(id);
        if (unit == null)
        {
            return NotFound(id);
        }
        if (id != unit.Id)
        {
            return BadRequest(new Error("Wrong Id", "You are requesting a different id than the unit you are trying to modify."));
        }

        if (CurrentUser(User) != unit.Creator)
        {
            return Unauthorized();
        }

        string oldName = unit.Name;
        int oldOrderNumber = unit.OrderNumber;
        unit.UpdateWithDto(unitDto);
        bool changeOrder = await ChangeOrder(oldOrderNumber, unit.OrderNumber);
        if (!changeOrder)
        {
            unit.OrderNumber = oldOrderNumber;
        }

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
        var deck = await _context.decks.FindAsync(unitDto.DeckId);
        var unit = unitDto.GetUnit(CurrentUser(User));
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
        var unit = await _context.units.FindAsync(id);
        if (unit == null)
        {
            return NotFound();
        }

        if (CurrentUser(User) != unit.Creator)
        {
            return Unauthorized();
        }

        _context.units.Remove(unit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UnitExists(int id)
    {
        return _context.units.Any(e => e.Id == id);
    }

    private bool IsUnique(Unit unit)
    {
        return !_context.units.Any(e => e.Name == unit.Name && e.DeckId == unit.DeckId);
    }


    private async Task<bool> ChangeOrder(int start, int target)
    {
        if (target == start)
        {
            return true;
        }

        if (target < 0 || start <= 0)
        {
            return false;
        }

        if (target == 0)
        {
            var units = await _context.units.Where(e => e.OrderNumber > target).ToListAsync();
            foreach (var unit in units)
            {
                unit.OrderNumber--;
                _context.Entry(unit).State = EntityState.Modified;
            }
        }

        else if (target > start)
        {
            var targetUnit = await _context.units.FirstOrDefaultAsync(e => e.OrderNumber == start);
            if (targetUnit == null)
            {
                return false;
            }
            targetUnit.OrderNumber = target;

            var units = await _context.units.Where(e => e.OrderNumber > start && e.OrderNumber <= target).ToListAsync();
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
