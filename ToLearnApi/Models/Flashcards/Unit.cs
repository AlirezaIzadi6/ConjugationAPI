namespace ToLearnApi.Models.Flashcards;

public class Unit
{
    public int Id { get; set;  }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DeckId { get; set; }
    public Deck Deck { get; set; }
    public List<Card> Cards { get; } = new();
}
