using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Conjugation;

public class Conjugation
{
    public int Id { get; set; }
    public string Infinitive { get; set; } = string.Empty;
    public string InfinitiveEnglish { get; set; } = string.Empty;
    public string Mood { get; set; } = string.Empty;
    public string MoodEnglish { get; set; } = string.Empty;
    public string Tense { get; set; } = string.Empty;
    public string TenseEnglish { get; set; } = string.Empty;
    public string VerbEnglish { get; set; } = string.Empty;
    public string? Form1S { get; set; }
    public string? Form2S { get; set; }
    public string? Form3S { get; set; }
    public string? Form1P { get; set; }
    public string? Form2P { get; set; }
    public string? Form3P { get; set; }
    public string Gerund { get; set; } = string.Empty;
    public string GerundEnglish { get; set; } = string.Empty;
    public string PastParticiple { get; set; } = string.Empty;
    public string PastParticipleEnglish { get; set; } = string.Empty;
}