using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConjugationAPI.Contexts;
using ConjugationAPI.Models.Conjugation;

namespace ConjugationAPI.Controllers;

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
        return await _context.Profiles.Where(e => e.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
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

        if (!profile.CheckUser(User))
        {
            return Unauthorized();
        }
        return profile;
    }

    // PUT: api/Profiles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutProfile(int id, Profile profile)
    {
        if (id != profile.Id ||
            !(InfinitivesIsValid(profile.Infinitives) &&
            MoodsIsValid(profile.Moods) &&
            PersonsIsValid(profile.Persons)))
        {
            return BadRequest();
        }

        if (!profile.CheckUser(User))
        {
            return Unauthorized(profile);
        }

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
        if (InfinitivesIsValid(profile.Infinitives) &&
            MoodsIsValid(profile.Moods) &&
            PersonsIsValid(profile.Persons))
        {
            profile.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }
        return BadRequest();
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

        if (!profile.CheckUser(User))
        {
            return Unauthorized();
        }

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProfileExists(int id)
    {
        return _context.Profiles.Any(e => e.Id == id);
    }

    private bool InfinitivesIsValid(string value)
    {
        if (value == "all")
        {
            return true;
        }
        string[] infinitives = value.Split(',');
        foreach (string infinitive in infinitives)
        {
            if (!_context.conjugations.Any(e => e.Infinitive == infinitive))
            {
                return false;
            }
        }
        return true;
    }

    private bool MoodsIsValid(string value)
    {
        if (value == "all")
        {
            return true;
        }
        string[] moods = value.Split(',');
        foreach (var mood in moods)
        {
            if (!_context.conjugations.Any(e => e.Mood == mood))
            {
                return false;
            }
        }
        return true;
    }

    private bool PersonsIsValid(string value)
    {
        if (value == "all")
        {
            return true;
        }
        string[] persons = value.Split(',');
        List<string> validPersons = new() { "1s", "2s", "3s", "1p", "2p", "3p" };
        foreach (var person in persons)
        {
            if (!validPersons.Contains(value))
            {
                return false;
            }
        }
        return true;
    }
}
