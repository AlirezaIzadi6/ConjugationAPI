using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Conjugation;
using ToLearnApi.Models.General;

namespace ToLearnApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController : MyController
{
    private readonly ApplicationDbContext _context;

    public ProfilesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Profiles
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProfileDto>>> GetProfiles()
    {
        var profiles = await _context.Profiles.Where(e => e.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
        List<ProfileDto> profileDtos = new();
        foreach (var profile in profiles)
        {
            profileDtos.Add(profile.GetDto());
        }
        return Ok(profileDtos);
    }

    // GET: api/Profiles/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ProfileDto>> GetProfile(int id)
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
        return profile.GetDto();
    }

    // PUT: api/Profiles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutProfile(int id, ProfileDto profileDto)
    {
        var profile = _context.Profiles.Find(id);
        if (profile == null)
        {
            return NotFound();
        }
        profile.UpdateWithDto(profileDto);

        if (id != profile.Id)
        {
            return BadRequest(new Error("Wrong Id", "You are requesting a different id than the profile you are trying to modify."));
        }
        if (!InfinitivesIsValid(profile.Infinitives))
        {
            return BadRequest(new Error("Infinitives not found", "You have entered some invalid/not-existing infinitive(s). Make sure you have separated your infinitives with a comma and check spelling errors and try again."));
        }
        if (!MoodsIsValid(profile.Moods))
        {
            return BadRequest(new Error("Wrong mood/tenses", "Check for moods and/or tenses errors and try again."));
        }
        if (!PersonsIsValid(profile.Persons))
        {
            return BadRequest(new Error("Wrong persons", "You have requested invalid persons."));
        }

        if (!profile.CheckUser(User))
        {
            return Unauthorized(profileDto);
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
    public async Task<ActionResult<ProfileDto>> PostProfile(ProfileDto profileDto)
    {
        var profile = profileDto.GetProfile(CurrentUser(User));

        if (!InfinitivesIsValid(profile.Infinitives))
        {
            return BadRequest(new Error("Infinitives not found", "You have entered some invalid/not-existing infinitive(s). Make sure you have separated your infinitives with a comma and check spelling errors and try again."));
        }
        if (!MoodsIsValid(profile.Moods))
        {
            return BadRequest(new Error("Wrong mood/tenses", "Check for moods and/or tenses errors and try again."));
        }
        if (!PersonsIsValid(profile.Persons))
        {
            return BadRequest(new Error("Wrong persons", "You have requested invalid persons."));
        }

        profile.UserId = CurrentUser(User);
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProfile", new { id = profile.Id }, profileDto);
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
        foreach (var moodAndTense in moods)
        {
            string[] moodParsed = moodAndTense.Split('-');
            if (moodParsed.Length != 2)
            {
                return false;
            }
            string mood = moodParsed[0];
            string tense = moodParsed[1];
            if (!_context.conjugations.Any(e => e.Mood == mood && e.Tense == tense))
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
            if (!validPersons.Contains(person))
            {
                return false;
            }
        }
        return true;
    }
}
