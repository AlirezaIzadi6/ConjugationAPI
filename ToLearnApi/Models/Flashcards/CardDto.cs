using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class CardDto
{
    public int Id { get; set; }
    [Required]
    public required string Question { get; set; }
    [Required]
    public required string Answer { get; set; }
    public required string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
    [Required]
    public int UnitId { get; set; }

    public Card GetCard(string userId)
    {
        return new()
        {
            Creator = userId,
            Question = Question,
            Answer = Answer,
            Description = Description,
            OrderNumber = OrderNumber,
            UnitId = UnitId
        };
    }
}
