using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards.LearnAndReview;

public class LearnStatus
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int DeckId { get; set; }
    public int UnitId { get; set; }
    public bool IsInitialized { get; set; }
    public bool IsFinished { get; set; } = false;
}
