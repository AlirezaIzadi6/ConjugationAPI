using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ToLearnApi.Models.Identity;

[Table("users", Schema = "identity")]
public class CustomUser : IdentityUser
{
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public int Score { get; set; } = 0;
}

[Table("roles", Schema = "identity")]
public class CustomRole : IdentityRole
{
}

[Table("userRole", Schema = "identity")]
public class CustomUserRole : IdentityUserRole<string>
{
}

[Table("userLogins", Schema = "identity")]
public class CustomUserLogin : IdentityUserLogin<string>
{
}

[Table("userClaims", Schema = "identity")]
public class CustomUserClaim : IdentityUserClaim<string>
{
}

[Table("tokens", Schema = "identity")]
public class CustomUserToken : IdentityUserToken<string>
{
}

[Table("roleClaims", Schema = "identity")]
public class CustomRoleClaim : IdentityRoleClaim<string>
{
}
