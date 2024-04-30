using Microsoft.EntityFrameworkCore;

namespace ConjugationAPI.Models;

public class ConjugationContext : DbContext
{
    public DbSet<Conjugation> conjugations { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Question> questions { get; set; }
    public DbSet<Answer> answers { get; set; }
    public DbSet<UserScore> Userscores { get; set; }

    public ConjugationContext(DbContextOptions<ConjugationContext> options)
        : base(options)
    {
    }
}
