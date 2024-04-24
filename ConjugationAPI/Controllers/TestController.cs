using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConjugationAPI.Models;

namespace ConjugationAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ConjugationContext _context;
    public TestController(ConjugationContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/random")]
    [Authorize]
    public async Task<ActionResult<Question>> GetRandomQuestion(int id)
    {
        if (!_context.Profiles.Any(e => e.ProfileId == id)) return NotFound();

        Profile profile = _context.Profiles.First(e => e.ProfileId == id);
        if (!profile.CheckUser(User)) return Unauthorized();
        Profile defaultProfile = _context.Profiles.First(e => e.Name == "default");
        Random rand = new();
        List<string> infinitives = new();
        List<string> moods = new();
        List<string> persons = new();

        if (profile.Infinitives == "all") infinitives = defaultProfile.Infinitives.Split(',').ToList();
        else infinitives = profile.Infinitives.Split(',').ToList();
        if (profile.Moods == "all") moods = defaultProfile.Moods.Split(',').ToList();
        else moods = profile.Moods.Split(',').ToList();
        if (profile.Persons == "all") persons = defaultProfile.Persons.Split(',').ToList();
        else persons = profile.Persons.Split(',').ToList();

        string infinitive = infinitives.ElementAt(rand.Next(0, infinitives.Count));
        string mood = moods.ElementAt(rand.Next(0, moods.Count));
        string person = persons.ElementAt(rand.Next(0, persons.Count));
        Question question = new() { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), infinitive = infinitive, Person = person, Mood = mood };
        return question;
    }
}
