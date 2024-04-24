using System.ComponentModel.DataAnnotations;

namespace ConjugationAPI.Models;

public class Question
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string infinitive { get; set; } = string.Empty;
    public string Mood { get; set; } = string.Empty;
    public string Person {  get; set; } = string.Empty;
    public bool HasBeenAnswered { get; set; } = false;
}
