using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards.LearnAndReview;

public class FlashcardAnswer
{
    public int ItemId { get; set; }
    [StringLength(100)]
    public required string AnswerText { get; set; } = string.Empty;
}
