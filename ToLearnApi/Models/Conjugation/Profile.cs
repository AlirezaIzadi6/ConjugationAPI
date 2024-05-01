using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ToLearnApi.Models.Conjugation;

public class Profile
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Moods { get; set; } = string.Empty;
    public string Infinitives {  get; set; } = string.Empty;
    public string Persons {  get; set; } = string.Empty;

    public bool CheckUser(ClaimsPrincipal user)
    {
        if (this.UserId == user.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return true;
        }
        return false;
    }
}
