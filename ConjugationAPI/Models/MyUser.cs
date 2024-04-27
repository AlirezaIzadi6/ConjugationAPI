using Microsoft.AspNetCore.Identity;

namespace ConjugationAPI.Models;

public class MyUser : IdentityUser
{
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}
