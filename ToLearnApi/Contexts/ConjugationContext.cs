using Microsoft.EntityFrameworkCore;
using ToLearnApi.Models.Conjugation;

namespace ToLearnApi.Contexts;

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
