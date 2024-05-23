using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Flashcards;

public class Card
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
    [Required]
    public int UnitId { get; set; }
    public Unit Unit { get; set; }

    public CardDto GetDto()
    {
        return new()
        {
            Id = Id,
            Question = Question,
            Answer = Answer,
            Description = Description,
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
