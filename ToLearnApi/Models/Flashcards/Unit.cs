using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class Unit
{
    public int Id { get; set;  }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
    [Required]    
    public int DeckId { get; set; }
    public Deck Deck { get; set; }
    public List<Card> Cards { get; } = new();

    public UnitDto GetDto()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            OrderNumber = OrderNumber,
            DeckId = DeckId
        };
    }

    public void UpdateWithDto(UnitDto dto)
    {
        Name = dto.Name;
        Description = dto.Description;
    }
}
