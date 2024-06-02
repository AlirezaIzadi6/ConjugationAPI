using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Identity;

public class RegisterRequest
{
    public required string Email { get; init; }
    [StringLength(32)]
    public required string UserName { get; init; }
    public required string Password { get; init; }
}
