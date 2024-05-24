using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class CardDto
{
    public int Id { get; set; }
    [Required]
    public string Question { get; set; }
    [Required]
    public string Answer { get; set; }
    public string Description { get; set; }
    public int OrderNumber { get; set; }
    [Required]
    public int UnitId { get; set; }

    public Card GetCard()
    {
        return new()
        {
            Question = Question,
            Answer = Answer,
            Description = Description,
            OrderNumber = OrderNumber,
            UnitId = UnitId
        };
    }
}
