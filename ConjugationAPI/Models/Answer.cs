using Microsoft.EntityFrameworkCore;

namespace ConjugationAPI.Models;

public class Answer
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Infinitive { get; set; } = string.Empty;
    public string Mood { get; set; } = string.Empty;
    public string Person {  get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = false;
}
