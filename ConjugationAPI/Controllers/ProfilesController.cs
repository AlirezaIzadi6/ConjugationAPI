using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConjugationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ConjugationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ConjugationContext _context;

        public ProfilesController(ConjugationContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _context.Profiles.Where(q => q.CheckUser(User)).ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            if (!profile.CheckUser(User)) return Unauthorized();
            return profile;
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProfile(int id, Profile profile)
        {
            if (id != profile.ProfileId)
            {
                return BadRequest();
            }

            if (!profile.CheckUser(User)) return Unauthorized(profile);

            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
            profile.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = profile.ProfileId }, profile);
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            if (!profile.CheckUser(User)) return Unauthorized();
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileId == id);
        }
    }
}
