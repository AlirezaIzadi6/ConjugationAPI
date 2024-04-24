namespace ConjugationAPI.Models;

public class QuestionDTO
{
    public int Id { get; set; } = 0;
    public string UserId { get; set; } = string.Empty;
    public string infinitive { get; set; } 
    public string Mood { get; set; } = string.Empty;
    public string Person { get; set; } = string.Empty;
}
