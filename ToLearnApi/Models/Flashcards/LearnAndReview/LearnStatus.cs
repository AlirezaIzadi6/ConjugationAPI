using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Models.Flashcards.LearnAndReview;

public class LearnStatus
{
    public int Id { get; set; }
    [ForeignKey(nameof(CustomUser))]
    public required string UserId { get; set; }
    public CustomUser User { get; set; }
    public required int DeckId { get; set; }
    public required int UnitId { get; set; }
    public required bool IsInitialized { get; set; }
    public required bool IsFinished { get; set; } = false;
}
