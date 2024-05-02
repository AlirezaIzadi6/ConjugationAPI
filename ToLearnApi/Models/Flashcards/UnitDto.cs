using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class UnitDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public int DeckId { get; set; }

    public Unit GetUnit()
    {
        return new Unit
        {
            Name = Name,
            Description = Description,
            DeckId = DeckId
        };
    }
}
