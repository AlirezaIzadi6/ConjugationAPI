namespace ToLearnApi.Models.Flashcards.LearnAndReview;

public class FlashcardAnswer
{
    public int ItemId { get; set; }
    public required string AnswerText { get; set; } = string.Empty;
}
