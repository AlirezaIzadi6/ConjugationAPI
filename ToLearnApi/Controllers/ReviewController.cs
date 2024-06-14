using Microsoft.AspNetCore.Authorization;
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

    // GET: api/review/4
    [HttpGet("{deckId}")]
    [Authorize]
    public async Task<ActionResult<List<FlashcardQuestion>>> GetQuestions(int deckId, int count)
    {
        // Find and sort all learned items for this deck that their next review time has reached.
        var reviewItems = _context.items.Where(e => e.Learned && e.DeckId == deckId && e.NextReview <= DateTime.Now)
            .OrderBy(e => e.NextReview)
            .ToList();

        var questions = new List<FlashcardQuestion>(); // List to store questions returned to the user.

        // Until reaching count limit, for each item find its card and create FlashcardQuestion for it.
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

    // POST: api/review/4
    [HttpPost("{deckId}")]
    [Authorize]
    public async Task<ActionResult> PostAnswer(int deckId,  FlashcardAnswer answer)
    {
        // Ensure requested deck exists.
        var deck = await _context.decks.FindAsync(deckId);
        if (deck == null)
        {
            return NotFound();
        }

        // Find requested item and check for errors.
        var item = await _context.items.FindAsync(answer.ItemId);
        if (item == null)
        {
            return BadRequest(new Error("Wrong Id", "The item Id you have requested has not found."));
        }

        if (!item.Learned)
        {
            return BadRequest(new Error("Item not learned", "This item has not been learned yet."));
        }

        // Find item card and check answer.
        var card = await _context.cards.FindAsync(item.CardId);
        if (card == null)
        {
            return BadRequest(new Error("Data error", "Card does not exist."));
        }

        if (answer.AnswerText != card.Answer)
        {
            return Ok("Wrong");
        }

        // Answer was not wrong, so update LastReview and NextReview time, and increase NumberOfReview.
        item.LastReview = DateTime.Now;
        item.NumberOfReviews++;
        // Each time next review interval doubles, so we do 2 to the power of number of reviews to set interval days.
        TimeSpan days = new((int)Math.Pow(2, item.NumberOfReviews), 0, 0, 0);
        item.NextReview = item.LastReview.Add(days);
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok("Right");
    }

    // GET: api/review/getanswer/10
    [HttpGet("getanswer/{itemId}")]
    [Authorize]
    public async Task<ActionResult<CardDto>> GetAnswer(int itemId)
    {
        // Find requested item and if exists, return DTO of its card.
        var item = await _context.items.FindAsync(itemId);

        if (item == null)
        {
            return NotFound();
        }

        var card = await _context.cards.FindAsync(item.CardId);

        return Ok(card.GetDto());
    }

    // GET: api/review/reset/10
    [HttpGet("reset/{itemId}")]
    [Authorize]
    public async Task<ActionResult<CardDto>> ResetItem(int itemId)
    {
        // Find requested item and check existance. If exists, check if this user have learnStatus related to this item's deck.
        var item = await _context.items.FindAsync(itemId);

        if (item == null)
        {
            return NotFound();
        }

        if (item.UserId != CurrentUser(User))
        {
            return Unauthorized();
        }

        // Reset item and save.
        item.NumberOfReviews = 0;
        item.LastReview = DateTime.Now;
        item.LearnedAt = DateTime.Now;
        item.NextReview = DateTime.Now.AddDays(1);
        _context.Entry(item).State = EntityState.Modified;

        _context.SaveChanges();

        // Return reset item's card DTO.
        var card = await _context.cards.FindAsync(item.CardId);

        return Ok(card.GetDto());
    }
}
