using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class UnitDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
    [Required]
    public int DeckId { get; set; }

    public Unit GetUnit(string userId)
    {
        return new Unit
        {
            Creator = userId,
            Name = Name,
            Description = Description,
            OrderNumber = OrderNumber,
            DeckId = DeckId
        };
    }
}
