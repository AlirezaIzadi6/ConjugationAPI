using Microsoft.EntityFrameworkCore;

namespace ConjugationAPI.Models.Conjugation;

public class UserScore
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int Score { get; set; } = 0;
}
