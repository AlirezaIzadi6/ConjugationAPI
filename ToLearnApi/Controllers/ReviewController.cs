using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.Flashcards.LearnAndReview;
using ToLearnApi.Models.General;

namespace ToLearnApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : MyController
{
    private readonly ConjugationContext _context;

    public ReviewController(ConjugationContext context)
    {
        _context = context;
    }

    [HttpGet("{deckId}")]
    [Authorize]
    public async Task<ActionResult<List<Question>>> GetQuestions(int deckId, int count)
    {
        var reviewItems = _context.items.Where(e => e.DeckId == deckId && e.NextReview <= DateTime.Now)
            .OrderBy(e => e.NextReview)
            .ToList();

        var questions = new List<Question>();
        foreach (var item in reviewItems)
        {
            if (questions.Count == count)
            {
                break;
            }

            var card = await _context.cards.FindAsync(item.CardId);
            questions.Add(new Question()
            {
                ItemId = item.Id,
                QuestionText = card.Question
            });
        }
        return questions;
    }

    [HttpPost("{deckId}")]
    public async Task<ActionResult> PostAnswer(int deckId,  Answer answer)
    {
        var deck = await _context.decks.FindAsync(deckId);
        if (deck == null)
        {
            return NotFound();
        }

        var item = await _context.items.FindAsync(answer.ItemId);
        if (item == null)
        {
            return BadRequest(new Error("Wrong Id", "The item Id you have requested has not found."));
        }

        if (!item.Learned)
        {
            return BadRequest(new Error("Item not learned", "This item has not been learned yet."));
        }

        var card = await _context.cards.FindAsync(item.CardId);
        if (answer.AnswerText != card.Answer)
        {
            return Ok("Wrong");
        }

        item.LastReview = DateTime.Now;
        item.NumberOfReviews++;
        TimeSpan days = new((int)Math.Pow(2, item.NumberOfReviews), 0, 0, 0);
        item.NextReview = item.LastReview.Add(days);
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok("Right");
    }
}
