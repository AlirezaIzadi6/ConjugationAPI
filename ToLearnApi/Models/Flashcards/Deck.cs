using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class Deck
{
    public int Id { get; set; }
    public string Creator { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Unit> Units {  get; } = new List<Unit>();
}
