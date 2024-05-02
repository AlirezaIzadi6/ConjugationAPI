namespace ToLearnApi.Models.Flashcards;

public class Unit
{
    public int Id { get; set;  }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DeckId { get; set; }
    public Deck Deck { get; set; } = new()
    {
        Title = "New deck",
        Description = string.Empty
    };
    public List<Card> Cards { get; } = new();
}
