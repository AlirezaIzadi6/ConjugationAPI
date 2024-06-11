using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToLearnApi.Models.Flashcards.LearnAndReview;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Models.Flashcards;

public class Card
{
    public int Id { get; set; }
    [ForeignKey(nameof(CustomUser))]
    public required string UserId { get; set; } = string.Empty;
    public CustomUser User { get; set; }
    [StringLength(8000)]
    public required string Question { get; set; } = string.Empty;
    [StringLength(100)]
    public required string Answer { get; set; } = string.Empty;
    [StringLength(8000)]
    public required string Description { get; set; } = string.Empty;
    public required int OrderNumber { get; set; }
    [Required]
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public List<Item> Items { get; } = new();

    public CardDto GetDto()
    {
        return new()
        {
            Id = Id,
            Question = Question,
            Answer = Answer,
            Description = Description,
            OrderNumber = OrderNumber,
            UnitId = UnitId
        };
    }

    public void UpdateWithDto(CardDto dto)
    {
        Question = dto.Question;
        Answer = dto.Answer;
        Description = dto.Description;
    }
}
