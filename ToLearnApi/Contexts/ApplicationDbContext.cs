using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Models.Conjugation;
using ToLearnApi.Models.Flashcards.LearnAndReview;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Contexts;

public class ApplicationDbContext : IdentityDbContext<CustomUser, CustomRole, string, CustomUserClaim, CustomUserRole, CustomUserLogin, CustomRoleClaim, CustomUserToken>
{
    // Conjugation models:
    public DbSet<Conjugation> conjugations { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Question> questions { get; set; }
    public DbSet<Answer> answers { get; set; }

    // Flashcards models:
    public DbSet<Deck> decks { get; set; }
    public DbSet<Unit> units { get; set; }
    public DbSet<Card> cards { get; set; }
    public DbSet<Item> items { get; set; }
    public DbSet<LearnStatus> learnStatuses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        string identitySchema = "security";
        // Identity tables:
        modelBuilder.Entity<CustomUser>(b =>
        {
            b.ToTable("users", identitySchema);
        });

        modelBuilder.Entity<CustomRole>(b =>
        {
            b.ToTable("roles", identitySchema);
        });

        modelBuilder.Entity<CustomUserLogin>(b =>
        {
            b.ToTable("userLogins",     identitySchema);
        });

        modelBuilder.Entity<CustomUserRole>(b =>
        {
            b.ToTable("userRoles", identitySchema);
        });

        modelBuilder.Entity<CustomUserClaim>(b =>
        {
            b.ToTable("userClaims", identitySchema);
        });

        modelBuilder.Entity<CustomRoleClaim>(b =>
        {
            b.ToTable("roleClaims", identitySchema);
        });

        modelBuilder.Entity<CustomUserToken>(b =>
        {
            b.ToTable("userTokens", identitySchema);
        });

        // Conjugation models:
        modelBuilder.Entity<Conjugation>(b =>
        {
            b.ToTable("conjugations", "conjugation");
        });

        modelBuilder.Entity<Profile>(b =>
        {
            b.ToTable("profiles", "conjugation");
        });


        modelBuilder.Entity<Question>(b =>
        {
            b.ToTable("questions", "conjugation");
        });

        modelBuilder.Entity<Answer>(b =>
        {
            b.ToTable("answers", "conjugation");
        });

        // Flashcards models:
        modelBuilder.Entity<Deck>(b =>
        {
            b.ToTable("decks", "flashcards");
        });

        modelBuilder.Entity<Unit>(b =>
        {
            b.ToTable("units", "flashcards");
        });

        modelBuilder.Entity<Card>(b =>
        {
            b.ToTable("cards", "flashcards");
        });

        modelBuilder.Entity<Item>(b =>
        {
            b.ToTable("items", "flashcards");
        });

        modelBuilder.Entity<LearnStatus>(b =>
        {
            b.ToTable("learnStatuses", "flashcards");
        });
    }
}
