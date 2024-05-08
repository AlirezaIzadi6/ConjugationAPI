using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToLearnApi.Controllers;

public class MyController : ControllerBase
{
    public static string CurrentUser(ClaimsPrincipal user)
    {
        string? currentUser = user.FindFirstValue(ClaimTypes.Email);
        return currentUser == null ? string.Empty : currentUser;
    }
}
