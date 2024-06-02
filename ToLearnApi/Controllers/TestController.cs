using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Conjugation;
using ToLearnApi.Models.General;

namespace ToLearnApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : MyController
{
    private readonly ApplicationDbContext _context;
    public TestController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get: api/test/5/random
    [HttpGet("{id}/random")]
    [Authorize]
    public async Task<ActionResult<QuestionDto>> GetRandomQuestion(int id)
    {
        // Find requested profile and check for errors.
        var profile = _context.Profiles.Find(id);

        if (profile == null)
        {
            return NotFound();
        }

        if (!profile.CheckUser(User))
        {
            return Unauthorized();
        }

        // Retrieve default profile to get complete list of infinitives, moods and tenses. If "all" keyword is used in request, use this profile instead of requested profile for that field.
        Profile defaultProfile = _context.Profiles.First(e => e.Name == "default");

        var infinitives = (profile.Infinitives == "all") ? defaultProfile.Infinitives.Split(',') : profile.Infinitives.Split(',');
        var moods = (profile.Moods == "all") ? defaultProfile.Moods.Split(',') : profile.Moods.Split(',');
        var persons = (profile.Persons == "all") ? defaultProfile.Persons.Split(',') : profile.Persons.Split(',');

        // Define required variables in following loop.
        string infinitive, moodAndTense, mood, tense, person = string.Empty;
        string? correctAnswer = string.Empty;

        while (true)
        {
            // Choose a random infinitive, mood, tense and person. If there is no conjugation with these data, loop continues getting new randoms to find one.
            Random rand = new();

            infinitive = infinitives[rand.Next(0, infinitives.Length)];
            moodAndTense = moods[rand.Next(0, moods.Length)];
            // moodAndTense is two parts separated by '-'. So we have to split them.
            mood = moodAndTense.Split('-')[0];
            tense = moodAndTense.Split('-')[1];
            person = persons[rand.Next(0, persons.Length)];

            // Get conjugation, and if exists, set correctAnswer based on person value. If doesn't exist, find new random values.
            Conjugation? conjugation = await _context.conjugations.FirstOrDefaultAsync(e => e.Infinitive == infinitive && e.Mood == mood && e.Tense == tense);
            if (conjugation == null)
            {
                continue;
            }

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
            if (!string.IsNullOrEmpty(correctAnswer)) // If there is a conjugation for our values
            {
                break;
            }
        }

        // Create, save and return a question based on found values.
        Question question = new()
        {
            UserId = CurrentUser(User),
            Infinitive = infinitive,
            Mood = moodAndTense,
            Person = person,
            HasBeenAnswered = false,
            Answer = correctAnswer
        };

        _context.questions.Add(question);
        await _context.SaveChangesAsync();

        return question.GetDto();
    }

    // POST: api/test/answer
    [HttpPost("answer")]
    [Authorize]
    public async Task<ActionResult> Examine(AnswerDto answerDto)
    {
        // Find requested question and check for errors.
        var question = await _context.questions.FindAsync(answerDto.QuestionId);
        if (question == null)
        {
            return NotFound();
        }

        if (question.UserId != CurrentUser(User))
        {
            return Unauthorized();
        }

        if (question.HasBeenAnswered)
        {
            return BadRequest(new Error("Not accepted", "You have answered this question before."));
        }

        // Create new answer object to save.
        Answer newAnswer = new()
        {
            AnswerText = answerDto.AnswerText,
            Question = question
        };

        _context.answers.Add(newAnswer);

        // Check answer and return 200 response with result text. Make question answered if answer is correct.
        string response;
        if (answerDto.AnswerText == question.Answer)
        {
            response = "Right";
            question.HasBeenAnswered = true;
            _context.Entry(question).State = EntityState.Modified;
        }

        else
        {
            response = "Wrong";
        }

        await _context.SaveChangesAsync();

        return Ok(response);
    }
}
