using Microsoft.EntityFrameworkCore;

namespace ConjugationAPI.Models;

public class ConjugationContext : DbContext
{
    public DbSet<Conjugation> conjugations { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<User> Users { get; set; }

    public ConjugationContext(DbContextOptions<ConjugationContext> options)
        : base(options)
    {
    }
}
