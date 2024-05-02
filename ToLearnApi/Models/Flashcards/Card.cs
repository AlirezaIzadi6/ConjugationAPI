namespace ToLearnApi.Models.Flashcards;

public class Card
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
}
