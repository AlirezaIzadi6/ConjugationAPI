using System.ComponentModel.DataAnnotations;

namespace ConjugationAPI.Models;

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
}
