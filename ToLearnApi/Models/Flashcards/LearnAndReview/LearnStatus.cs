using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards.LearnAndReview;

public class LearnStatus
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required int DeckId { get; set; }
    public required int UnitId { get; set; }
    public required bool IsInitialized { get; set; }
    public required bool IsFinished { get; set; } = false;
}
