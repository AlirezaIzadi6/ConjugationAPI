using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Flashcards;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDto>>> Getcards()
        {
            var cards = await _context.cards.ToListAsync();
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
                return BadRequest();
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
                return BadRequest();
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
