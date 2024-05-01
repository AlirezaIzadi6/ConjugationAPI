using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConjugationAPI.Models.Identity;

namespace ConjugationAPI.Contexts;

public class ApplicationDbContext : IdentityDbContext<MyUser>
{
    public DbSet<MyUser> myUsers { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { 
    }
}
