// Parent controller for program controllers, to hold shared methods.

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ToLearnApi.Controllers;

public class MyController : ControllerBase
{
    // Get current user UserId, and return empty string if user is not logged in.
    public static string CurrentUser(ClaimsPrincipal user)
    {
        string? currentUser = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return currentUser == null ? string.Empty : currentUser;
    }
}
