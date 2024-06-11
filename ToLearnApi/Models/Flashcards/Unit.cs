using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Models.Flashcards;

public class Unit
{
    public int Id { get; set;  }
    [ForeignKey(nameof(CustomUser))]
    public string UserId { get; set; }
    public CustomUser User { get; set; }
    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(4000)]
    public required string Description { get; set; }
    public required int OrderNumber { get; set; }
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
