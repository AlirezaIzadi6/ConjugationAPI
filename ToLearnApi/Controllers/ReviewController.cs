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
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{deckId}")]
    [Authorize]
    public async Task<ActionResult<List<FlashcardQuestion>>> GetQuestions(int deckId, int count)
    {
        var reviewItems = _context.items.Where(e => e.DeckId == deckId && e.NextReview <= DateTime.Now)
            .OrderBy(e => e.NextReview)
            .ToList();

        var questions = new List<FlashcardQuestion>();
        foreach (var item in reviewItems)
        {
            if (questions.Count == count)
            {
                break;
            }

            var card = await _context.cards.FindAsync(item.CardId);
            if (card != null)
            {
                questions.Add(new FlashcardQuestion()
                {
                    ItemId = item.Id,
                    QuestionText = card.Question
                });
            }
        }
        return questions;
    }

    [HttpPost("{deckId}")]
    public async Task<ActionResult> PostAnswer(int deckId,  FlashcardAnswer answer)
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
        if (card == null)
        {
            return BadRequest(new Error("Data error", "Card does not exist."));
        }

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
