namespace ConjugationAPI.Models.Conjugation;

public class QuestionDto
{
    public int Id { get; set; } = 0;
    public string infinitive { get; set; } = string.Empty;
    public string Mood { get; set; } = string.Empty;
    public string Person { get; set; } = string.Empty;
}
