using System.ComponentModel.DataAnnotations;
using ToLearnApi.Models.Flashcards.LearnAndReview;

namespace ToLearnApi.Models.Flashcards;

public class Card
{
    public int Id { get; set; }
    public string Creator { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
    [Required]
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public List<Item> Items { get; }

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
