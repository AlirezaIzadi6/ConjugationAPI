using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConjugationAPI.Models;

namespace ConjugationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConjugationsController : ControllerBase
    {
        private readonly ConjugationContext _context;

        public ConjugationsController(ConjugationContext context)
        {
            _context = context;
        }

        // GET: api/Conjugations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conjugation>>> Getconjugations()
        {
            return await _context.conjugations.ToListAsync();
        }

        // GET: api/Conjugations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conjugation>> GetConjugation(int id)
        {
            var conjugation = await _context.conjugations.FindAsync(id);

            if (conjugation == null)
            {
                return NotFound();
            }

            return conjugation;
        }

        // PUT: api/Conjugations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConjugation(int id, Conjugation conjugation)
        {
            if (id != conjugation.Id)
            {
                return BadRequest();
            }

            _context.Entry(conjugation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConjugationExists(id))
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

        // POST: api/Conjugations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conjugation>> PostConjugation(Conjugation conjugation)
        {
            _context.conjugations.Add(conjugation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConjugation", new { id = conjugation.Id }, conjugation);
        }

        // DELETE: api/Conjugations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConjugation(int id)
        {
            var conjugation = await _context.conjugations.FindAsync(id);
            if (conjugation == null)
            {
                return NotFound();
            }

            _context.conjugations.Remove(conjugation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConjugationExists(int id)
        {
            return _context.conjugations.Any(e => e.Id == id);
        }
    }
}
