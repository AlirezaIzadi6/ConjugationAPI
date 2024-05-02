using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class DeckDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Deck GetDeck(string UserId)
    {
        return new Deck()
        {
            Creator = UserId,
            Title = Title,
            Description = Description
        };
    }
}
