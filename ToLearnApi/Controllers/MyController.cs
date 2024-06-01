using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ToLearnApi.Controllers;

public class MyController : ControllerBase
{
    public static string CurrentUser(ClaimsPrincipal user)
    {
        string? currentUser = user.FindFirstValue(ClaimTypes.Email);
        return currentUser == null ? string.Empty : currentUser;
    }
}
