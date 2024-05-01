using Microsoft.AspNetCore.Identity;

namespace ConjugationAPI.Models.Identity;

public class MyUser : IdentityUser
{
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public int Score { get; set; } = 0;
}
