using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using ToLearnApi.Contexts;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.Flashcards.LearnAndReview;
using ToLearnApi.Models.General;

namespace ToLearnApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LearnController : MyController
{
    private readonly ConjugationContext _context;

    public LearnController(ConjugationContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CardDto>>> GetNewItem(int id, int count)
    {
        Unit learningUnit;
        var learnStatus = await _context.learnStatuses.FirstOrDefaultAsync(e => e.UserId == CurrentUser(User) && e.DeckId == id);

        if (learnStatus != null)
        {
            if (learnStatus.IsFinished)
            {
                return Ok("This deck does not have any new cards to learn.");
            }

            learningUnit = await _context.units.FindAsync(learnStatus.UnitId);
            if (!learnStatus.IsInitialized)
            {
                InitializeUnit(learningUnit, CurrentUser(User));
                learnStatus.IsInitialized = true;
                _context.Entry(learnStatus).State = EntityState.Modified;
            }
        }

        else
        {
            learningUnit = await _context.units.FirstOrDefaultAsync(e => e.OrderNumber == 1);
            var newLearnStatus = new LearnStatus()
            {
                UserId = CurrentUser(User),
                DeckId = id,
                UnitId = learningUnit.Id,
                IsInitialized = false
            };
            _context.learnStatuses.Add(newLearnStatus);
            InitializeUnit(learningUnit, CurrentUser(User));
            learnStatus.IsInitialized = true;
            _context.Entry(learnStatus).State = EntityState.Modified;
        }

        var cards = new List<Card>();
        var itemsNotLearned = await _context.items.Where(e => e.Card.UnitId == learningUnit.Id && e.Learned == false)
            .OrderBy(e => e.Card.OrderNumber)
            .ToListAsync();
        if (itemsNotLearned.Count() == 0)
        {
            var newLearningUnit = await _context.units.FirstOrDefaultAsync(e => e.OrderNumber == learningUnit.OrderNumber + 1);
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

        await _context.SaveChangesAsync();

        foreach (var item in itemsNotLearned)
        {
            if (cards.Count() == count)
            {
                break;
            }
            var itemCard = await _context.cards.FindAsync(item.CardId);
            cards.Add(itemCard);
        }

        var cardDtos = new List<CardDto>();
        foreach (var card in cards)
        {
            cardDtos.Add(card.GetDto());
        }

        return cardDtos;
    }

    private bool InitializeUnit(Unit unit, string userId)
    {
        var cards = _context.cards.Where(e => e.UnitId == unit.Id);
        foreach (var card in cards)
        {
            var newItem = new Item()
            {
                UserId = userId,
                Learned = false,
                NumberOfReviews = 0,
                LastReview = DateTime.Now,
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

    [HttpGet("{deckId}/learned/{cardId}")]
    [Authorize]
    public async Task<ActionResult> SetLearned(int deckId, int cardId)
    {
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

        item.Learned = true;
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{deckId}/learned")]
    [Authorize]
    public async Task<ActionResult<List<int>>> SetListLearned(int deckId, List<int> cardIds)
    {
        var result = new List<int>();
        foreach (var cardId in cardIds)
        {
            var item = await _context.items.FirstOrDefaultAsync(e => e.CardId == cardId);
            if (item != null
                && CurrentUser(User) == item.UserId
                && !item.Learned)
            {
                item.Learned = true;
                _context.Entry(item).State = EntityState.Modified;
                result.Add(cardId);
            }
        }

        await _context.SaveChangesAsync();

        return result;
    }
}
