using Microsoft.EntityFrameworkCore;
using ConjugationAPI.Models.Conjugation;

namespace ConjugationAPI.Contexts;

public class ConjugationContext : DbContext
{
    public DbSet<Conjugation> conjugations { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Question> questions { get; set; }
    public DbSet<Answer> answers { get; set; }

    public ConjugationContext(DbContextOptions<ConjugationContext> options)
        : base(options)
    {
    }
}
