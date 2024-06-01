using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ToLearnApi.Models.Identity;

public class CustomUser : IdentityUser
{
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public int Score { get; set; } = 0;
}

public class CustomRole : IdentityRole
{
}

public class CustomUserClaim : IdentityUserClaim<string>
{
}

public class CustomUserRole : IdentityUserRole<string>
{
}

public class CustomUserLogin : IdentityUserLogin<string>
{
}

public class CustomRoleClaim : IdentityRoleClaim<string>
{
}

public class CustomUserToken : IdentityUserToken<string>
{
}
