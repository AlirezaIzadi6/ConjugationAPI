using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    // PUT: api/Units/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
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

        string oldName = unit.Name;
        unit.UpdateWithDto(unitDto);
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
    public async Task<ActionResult<UnitDto>> PostUnit(UnitDto unitDto)
    {
        var unit = unitDto.GetUnit();
        if (!_context.decks.Any(e => e.Id == unit.DeckId))
        {
            return BadRequest(unitDto);
        }

        if (!IsUnique(unit))
        {
            return BadRequest(new Error("Duplicate name", "This name already exists."));
        }
        _context.units.Add(unit);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUnit", new { id = unit.Id }, unit.GetDto());
    }

    // DELETE: api/Units/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnit(int id)
    {
        var unit = await _context.units.FindAsync(id);
        if (unit == null)
        {
            return NotFound();
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
}
