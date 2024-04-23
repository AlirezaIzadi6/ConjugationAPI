using Microsoft.EntityFrameworkCore;

namespace ConjugationAPI.Models;

public class UserScore
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int Score { get; set; } = 0;
}
