using System.ComponentModel.DataAnnotations;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Models.Flashcards;

public class DeckDto
{
    public int Id { get; set; }
    public string Creator { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Title { get; set; } = string.Empty;
    [StringLength(4000)]
    public string Description { get; set; } = string.Empty;

    public Deck GetDeck(CustomUser user)
    {
        return new Deck()
        {
            Creator = user.UserName,
            Title = Title,
            Description = Description,
            User = user
        };
    }
}
