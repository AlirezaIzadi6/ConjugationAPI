using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Conjugation;

public class AnswerDto
{
    public int QuestionId { get; set; }
    [StringLength(100)]
    public string AnswerText { get; set; } = string.Empty;
}
