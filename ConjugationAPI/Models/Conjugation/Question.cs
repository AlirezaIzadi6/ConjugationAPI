using System.ComponentModel.DataAnnotations;

namespace ConjugationAPI.Models.Conjugation;

public class Question
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Infinitive { get; set; } = string.Empty;
    public string Mood { get; set; } = string.Empty;
    public string Person {  get; set; } = string.Empty;
    public string Answer {  get; set; } = string.Empty;
    public bool HasBeenAnswered { get; set; } = false;
    public DateTime TimeStamp { get; set; } = DateTime.Now;

    public QuestionDto GetDto()
    {
        return new QuestionDto()
        {
            Id = Id,
            infinitive = Infinitive,
            Mood = Mood,
            Person = Person
        };
    }
}
