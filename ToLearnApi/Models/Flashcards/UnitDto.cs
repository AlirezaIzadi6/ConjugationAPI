using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class UnitDto
{
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(4000)]
    public string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
    [Required]
    public int DeckId { get; set; }

    public Unit GetUnit(string userId)
    {
        return new Unit
        {
            UserId = userId,
            Name = Name,
            Description = Description,
            OrderNumber = OrderNumber,
            DeckId = DeckId
        };
    }
}
