namespace ToLearnApi.Models.Flashcards.LearnAndReview;

public class FlashcardQuestion
{
    public int ItemId { get; set; }
    public required string QuestionText { get; set; } = string.Empty;
}
