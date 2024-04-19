using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConjugationAPI.Models;

public class Profile
{
    [Key]
    public int ProfileId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Moods { get; set; } = string.Empty;
    public string Infinitives {  get; set; } = string.Empty;
}
