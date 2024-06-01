using System.ComponentModel.DataAnnotations.Schema;

namespace ToLearnApi.Models.Conjugation;

public class Answer
{
    public int Id { get; set; }
    [ForeignKey(nameof(Question))]
    public int QuestionId { get; set; } = 0;
    public string AnswerText { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public Question Question { get; init; } = new();

    public AnswerDto GetDto()
    {
        return new AnswerDto()
        {
            QuestionId = QuestionId,
            AnswerText = AnswerText
        };
    }
}
