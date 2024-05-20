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

namespace ToLearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : MyController
    {
        private readonly ConjugationContext _context;

        public CardsController(ConjugationContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet("unit{id}")]
        public async Task<ActionResult<IEnumerable<CardDto>>> Getcards(int id)
        {
            var cards = await _context.cards.Where(e => e.UnitId == id).ToListAsync();
            List<CardDto> cardDtos = new List<CardDto>();
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

            card.UpdateWithDto(cardDto);
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
        public async Task<ActionResult<CardDto>> PostCard(CardDto cardDto)
        {
            var card = cardDto.GetCard();
            if (!_context.units.Any(e => e.Id == card.UnitId))
            {
                return BadRequest(new Error("Wrong unit Id", "Unit with your requested Id has not found."));
            }
            _context.cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = card.Id }, card.GetDto());
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.cards.Any(e => e.Id == id);
        }
    }
}
