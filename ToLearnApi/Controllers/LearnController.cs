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
public class LearnController : MyController
{
    private readonly ApplicationDbContext _context;

    public LearnController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/learn/4
    [HttpGet("{deckId}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CardDto>>> GetNewItem(int deckId, int count)
    {
        Unit? learningUnit;

        // Find existing LearnStatus with this UserId and DeckId, to find out in which unit should we search for not-learned cards. If no one exists, create one.
        var learnStatus = await _context.learnStatuses.FirstOrDefaultAsync(e => e.UserId == CurrentUser(User) && e.DeckId == deckId);

        if (learnStatus != null)
        {
            if (learnStatus.IsFinished)
            {
                return Ok("This deck does not have any new cards to learn.");
            }

            // Find the unit which the user should learn now. If the unit doesn't exist, there is a problem in data. If exists, ensure it is initialized.
            learningUnit = await _context.units.FindAsync(learnStatus.UnitId);
            if (learningUnit == null)
            {
                return BadRequest(new Error("Data error", "Requested unit doesn't exist."));
            }

            if (!learnStatus.IsInitialized)
            {
                InitializeUnit(learningUnit, CurrentUser(User));
                learnStatus.IsInitialized = true;
                _context.Entry(learnStatus).State = EntityState.Modified;
            }
        }

        else
        {
            // No learning history, so initialize the first unit and create a new LearnStatus for this user/deck.
            learningUnit = await _context.units.FirstOrDefaultAsync(e => e.OrderNumber == 1 && e.DeckId == deckId);
            if (learningUnit == null)
            {
                return BadRequest(new Error("No unit", "This unit does not have any units."));
            }

            InitializeUnit(learningUnit, CurrentUser(User));
            learnStatus = new LearnStatus()
            {
                UserId = CurrentUser(User),
                DeckId = deckId,
                UnitId = learningUnit.Id,
                IsInitialized = true,
                IsFinished = false
            };
            _context.learnStatuses.Add(learnStatus);
        }

        var cards = new List<Card>(); // List to store cards that we want to return.

        // Find and sort items that are not learned yet. If there is no new item, finish the unit and set next unit as learning unit. If there is no new unit, finish the deck.
        var itemsNotLearned = await _context.items.Where(e => e.Card.UnitId == learningUnit.Id && e.Learned == false)
            .OrderBy(e => e.Card.OrderNumber)
            .ToListAsync();
        if (itemsNotLearned.Count() == 0)
        {
            var newLearningUnit = await _context.units.FirstOrDefaultAsync(e => e.OrderNumber == learningUnit.OrderNumber + 1 && e.DeckId == deckId);
            string response;
            if (newLearningUnit == null)
            {
                learnStatus.IsFinished = true;
                response = "Deck completed";
            }

            else
            {
                learnStatus.UnitId = newLearningUnit.Id;
                learnStatus.IsInitialized = false;
                response = "Unit completed";
            }
            _context.Entry(learnStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(response);
        }

        // No error occurred, so save LearnStatus changes.
        await _context.SaveChangesAsync();

        // Add first items that need review, to cards list.
        foreach (var item in itemsNotLearned)
        {
            if (cards.Count() == count)
            {
                break;
            }
            var itemCard = await _context.cards.FindAsync(item.CardId);
            if (itemCard != null)
            {
                cards.Add(itemCard);
            }
        }

        // Create DTOs list, and add CardDtos to return.
        var cardDtos = new List<CardDto>();
        foreach (var card in cards)
        {
            cardDtos.Add(card.GetDto());
        }

        return cardDtos;
    }

    // GET: api/learn/4/learned/25
    [HttpGet("{deckId}/learned/{cardId}")]
    [Authorize]
    public async Task<ActionResult> SetLearned(int deckId, int cardId)
    {
        // Find requested item, and check for errors.
        var item = await _context.items.FirstOrDefaultAsync(e => e.CardId == cardId);
        if (item == null)
        {
            return NotFound();
        }

        if (CurrentUser(User) != item.UserId)
        {
            return Unauthorized();
        }

        if (item.Learned)
        {
            return BadRequest(new Error("Duplicate answer", "This question has previously answered."));
        }

        // Set requested item to learned. Set NextReview time to next day.
        item.Learned = true;
        item.LearnedAt = DateTime.Now;
        TimeSpan oneDay = new(1, 0, 0, 0);
        item.NextReview = item.LearnedAt.Add(oneDay);
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok();
    }

    // POST: api/learn/4/learned
    [HttpPost("{deckId}/learned")]
    [Authorize]
    public async Task<ActionResult<List<int>>> SetListLearned(int deckId, List<int> cardIds)
    {
        // Check posted ids one by one, and for each one if there is no error, set as learned and return its id.
        var result = new List<int>(); // List to store ItemIds that have reviewed successfully.

        foreach (var cardId in cardIds)
        {
            var item = await _context.items.FirstOrDefaultAsync(e => e.CardId == cardId);
            if (item != null
                && CurrentUser(User) == item.UserId
                && !item.Learned)
            {
                item.Learned = true;
                item.LearnedAt = DateTime.Now;
                TimeSpan oneDay = new(1, 0, 0, 0);
                item.NextReview = item.LearnedAt.Add(oneDay);
                _context.Entry(item).State = EntityState.Modified;
                result.Add(cardId);
            }
        }

        await _context.SaveChangesAsync();

        return result;
    }

    private bool InitializeUnit(Unit unit, string userId)
    {
        // Retrieve all cards of requested unit and for each one, create an item for requested user.
        var cards = _context.cards.Where(e => e.UnitId == unit.Id);

        foreach (var card in cards)
        {
            var newItem = new Item()
            {
                UserId = userId,
                Learned = false,
                LearnedAt = DateTime.Now,
                NumberOfReviews = 0,
                LastReview = DateTime.Now,
                NextReview = DateTime.Now,
                DeckId = unit.DeckId,
                Card = card
            };
            _context.items.Add(newItem);
        }
        try
        {
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
