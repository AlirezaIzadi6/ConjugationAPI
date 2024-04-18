using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Conjugation
{
    [Key]
    public int Id { get; set; }
    public string Infinitive { get; set; } = null!;
    public string InfinitiveEnglish { get; set; } = null!;
    public string Mood { get; set; } = null!;
    public string MoodEnglish { get; set; } = null!;
    public string Tense { get; set; } = null!;
    public string TenseEnglish { get; set; } = null!;
    public string VerbEnglish { get; set; } = null!;
    public string? Form1S { get; set; }
    public string? Form2S { get; set; }
    public string? Form3S { get; set; }
    public string? Form1P { get; set; }
    public string? Form2P { get; set; }
    public string? Form3P { get; set; }
    public string Gerund { get; set; } = null!;
    public string GerundEnglish { get; set; } = null!;
    public string PastParticiple { get; set; } = null!;
    public string PastParticipleEnglish { get; set; } = null!;
}