using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class CardDto
{
    public int Id { get; set; }
    [Required, StringLength(8000)]
    public required string Question { get; set; }
    [Required, StringLength(100)]
    public required string Answer { get; set; }
    [StringLength(8000)]
    public required string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
    [Required]
    public int UnitId { get; set; }

    public Card GetCard(string userId)
    {
        return new()
        {
            UserId = userId,
            Question = Question,
            Answer = Answer,
            Description = Description,
            OrderNumber = OrderNumber,
            UnitId = UnitId
        };
    }
}
