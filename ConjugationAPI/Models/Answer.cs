using Microsoft.EntityFrameworkCore;

namespace ConjugationAPI.Models;

public class Answer
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int QuestionId { get; set; } = 0;
    public string AnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = false;
}
