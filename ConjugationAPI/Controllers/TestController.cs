using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using ConjugationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace ConjugationAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ConjugationContext _context;
    private readonly ApplicationDbContext _applicationDbContext;
    public TestController(ConjugationContext context, ApplicationDbContext applicationDbContext)
    {
        _context = context;
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet("{id}/random")]
    [Authorize]
    public async Task<ActionResult<QuestionDto>> GetRandomQuestion(int id)
    {
        if (!_context.Profiles.Any(e => e.Id == id))
        {
            return NotFound();
        }

        Profile profile = _context.Profiles.First(e => e.Id == id);
        if (!profile.CheckUser(User))
        {
            return Unauthorized();
        }
        Profile defaultProfile = _context.Profiles.First(e => e.Name == "default");
        Random rand = new();

        var infinitives = (profile.Infinitives == "all") ? defaultProfile.Infinitives.Split(',') : profile.Infinitives.Split(',');
        var moods = (profile.Moods == "all") ? defaultProfile.Moods.Split(',') : profile.Moods.Split(',');
        var persons = (profile.Persons == "all") ? defaultProfile.Persons.Split(',') : profile.Persons.Split(',');
        string infinitive, moodAndTense, mood, tense, person = string.Empty;
        string? correctAnswer = string.Empty;
        while (true)
        {
            infinitive = infinitives[rand.Next(0, infinitives.Length)];
            moodAndTense = moods[rand.Next(0, moods.Length)];
            mood = moodAndTense.Split('-')[0];
            tense = moodAndTense.Split('-')[1];
            Conjugation conjugation = _context.conjugations.First(e => e.Infinitive == infinitive && e.Mood == mood && e.Tense == tense);
            person = persons[rand.Next(0, persons.Length)];
            switch(person)
            {
                case "1s":
                    correctAnswer = conjugation.Form1S;
                    break;
                case "2s":
                    correctAnswer = conjugation.Form2S;
                    break;
                case "3s":
                    correctAnswer = conjugation.Form3S;
                    break;
                case "1p":
                    correctAnswer = conjugation.Form1P;
                    break;
                case "2p":
                    correctAnswer = conjugation.Form2P;
                    break;
                case "3p":
                    correctAnswer = conjugation.Form3P;
                    break;
            }
            if (!correctAnswer.IsNullOrEmpty())
            {
                break;
            }
        }
        if (correctAnswer == null)
        {
            correctAnswer = string.Empty;
        }
        Question question = new()
        {
            UserId = CurrentUser(),
            Infinitive = infinitive,
            Mood = moodAndTense,
            Person = person,
            HasBeenAnswered = false,
            Answer = correctAnswer
        };
        _context.questions.Add(question);
        await _context.SaveChangesAsync();
        QuestionDto questionDto = question.GetDto();
        return questionDto;
    }

    [HttpPost("answer")]
    [Authorize]
    public async Task<ActionResult> Examine(AnswerDto answerDto)
    {
        Question? question = _context.questions.Find(answerDto.QuestionId);
        if (question == null)
        {
            return NotFound();
        }

        if (question.UserId == CurrentUser())
        {
            return Unauthorized();
        }

        if (question.HasBeenAnswered)
        {
            return BadRequest("This question has previously been answered by you.");
        }

        Answer newAnswer = new()
        {
            UserId = CurrentUser(),
            QuestionId = answerDto.QuestionId,
            AnswerText = answerDto.AnswerText
        };
        _context.answers.Add(newAnswer);
        if (!(answerDto.AnswerText == question.Answer))
        {
            return Ok("wrong");
        }

        question.HasBeenAnswered = true;
        _context.Entry(question).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        MyUser? currentUser = _applicationDbContext.Users.Find(CurrentUser());
        currentUser.Score += 5;
        _applicationDbContext.Entry(currentUser).State = EntityState.Modified;
        await _applicationDbContext.SaveChangesAsync();

        return Ok("right");
    }

    private string CurrentUser()
    {
        string? currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return currentUser == null ? string.Empty : currentUser;
    }
}
