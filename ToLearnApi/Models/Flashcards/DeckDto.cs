using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class DeckDto
{
    public int Id { get; set; }
    public string Creator { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Title { get; set; } = string.Empty;
    [StringLength(4000)]
    public string Description { get; set; } = string.Empty;

    public Deck GetDeck(string UserEmail)
    {
        return new Deck()
        {
            Creator = UserEmail,
            Title = Title,
            Description = Description
        };
    }
}
