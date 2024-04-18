using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConjugationAPI.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime LastLoginDate { get; set;}
}
