using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;
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
        // Find all existing profiles for current user and return a list of DTOs.
        var profiles = await _context.Profiles.Where(e => e.UserId == CurrentUser(User)).ToListAsync();
        var profileDtos = new List<ProfileDto>();
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
        // Find profile with requested id and check for errors. If no error, return its DTO.
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
        // Find requested profile to modify.
        var profile = _context.Profiles.Find(id);

        if (profile == null)
        {
            return NotFound();
        }

        // Update and check for errors.
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

        // No error, so save changes.
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
    public async Task<ActionResult<ProfileDto>> CreateProfile(ProfileDto profileDto)
    {
        // Create profile from request DTO, and check errors.
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

        // No error, so set UserId to current user and save profile.
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
        // Find requested profile and check for errors.
        var profile = await _context.Profiles.FindAsync(id);

        if (profile == null)
        {
            return NotFound();
        }

        if (!profile.CheckUser(User))
        {
            return Unauthorized();
        }

        // No error, so delete and save.
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
        // infinitives field must be "all", or a comma-separated list of infinitives that exist in the app database.
        if (value == "all")
        {
            return true;
        }

        // Get complete list of infinitives from default profile.
        var defaultProfile = _context.Profiles.First(e => e.Name == "default");
        var existingInfinitives = defaultProfile.Infinitives.Split(',')
            .ToList();
        string[] infinitives = value.Split(',');

        // All requested infinitives must exist in existingInfinitives list.
        foreach (string infinitive in infinitives)
        {
            if (!existingInfinitives.Contains(infinitive))
            {
                return false;
            }
        }

        return true;
    }

    private bool MoodsIsValid(string value)
    {
        // moods field must be "all", or a comma-separated list of mood-tenses that exist in the database. 
        if (value == "all")
        {
            return true;
        }

        // First create a list of mood-tenses.
        string[] moods = value.Split(',');

        // Get complete list of mood-tenses from default profile.
        var defaultProfile = _context.Profiles.First(e => e.Name == "default");
        var existingMoods = defaultProfile.Moods.Split(',')
            .ToList();

        // All requested values must exist in existingMoods list.
        foreach (var moodAndTense in moods)
        {
            if (!existingMoods.Contains(moodAndTense))
            {
                return false;
            }
        }

        return true;
    }

    private bool PersonsIsValid(string value)
    {
        // persons field must be "all", or a comma-separated list of valid persons.
        if (value == "all")
        {
            return true;
        }

        var validPersons = new List<string>() { "1s", "2s", "3s", "1p", "2p", "3p" };
        string[] persons = value.Split(',');
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
