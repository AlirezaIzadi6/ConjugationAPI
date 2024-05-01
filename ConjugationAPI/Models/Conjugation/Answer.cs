using Microsoft.EntityFrameworkCore;

namespace ConjugationAPI.Models.Conjugation;

public class Answer
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int QuestionId { get; set; } = 0;
    public string AnswerText { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;

    public AnswerDto GetDto()
    {
        return new AnswerDto()
        {
            QuestionId = QuestionId,
            AnswerText = AnswerText
        };
    }
}
