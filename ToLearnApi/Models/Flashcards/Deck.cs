using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Models.Flashcards;

public class Deck
{
    public int Id { get; set; }
    [ForeignKey(nameof(CustomUser))]
    public string UserId { get; set; }
    public CustomUser User { get; set; }
    public string Creator { get; set; } = string.Empty;
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    [StringLength(4000)]
    public string Description { get; set; } = string.Empty;
    public ICollection<Unit> Units {  get; } = new List<Unit>();

    public DeckDto GetDto()
    {
        return new DeckDto()
        {
            Id = Id,
            Creator = Creator,
            Title = Title,
            Description = Description
        };
    }

    public void UpdateWithDto(DeckDto dto)
    {
        Title = dto.Title;
        Description = dto.Description;
    }
}
