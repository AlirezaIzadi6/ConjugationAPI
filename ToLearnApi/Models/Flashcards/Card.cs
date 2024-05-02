namespace ToLearnApi.Models.Flashcards;

public class Card
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = new()
    {
        Name = "New unit",
        Description = string.Empty
    };
}
