using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class Deck
{
    public int Id { get; set; }
    public string Creator { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
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
