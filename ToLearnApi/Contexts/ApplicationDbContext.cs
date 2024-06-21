using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToLearnApi.Models.Conjugation;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.Flashcards.LearnAndReview;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Contexts;

public class ApplicationDbContext : IdentityDbContext<CustomUser, CustomRole, string, CustomUserClaim, CustomUserRole, CustomUserLogin, CustomRoleClaim, CustomUserToken>
{
    // Add dbsets except default identity models that IdentityDbContext includes:

    // Identity models:
    public virtual DbSet<UserScore> UserScores { get; set; }

    // Conjugation models:
    public virtual DbSet<Conjugation> conjugations { get; set; }
    public virtual DbSet<Profile> Profiles { get; set; }
    public virtual DbSet<Question> questions { get; set; }
    public virtual DbSet<Answer> answers { get; set; }

    // Flashcards models:
    public virtual DbSet<Deck> decks { get; set; }
    public virtual DbSet<Unit> units { get; set; }
    public virtual DbSet<Card> cards { get; set; }
    public virtual DbSet<Item> items { get; set; }
    public virtual DbSet<LearnStatus> learnStatuses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set custom table name and schema name for tables with FluentApi.
        base.OnModelCreating(modelBuilder);

        // Define names for db schemas:
        string identitySchemaName = "security";
        string conjugationSchemaName = "conjugation";
        string flashcardsSchemaName = "flashcards";

        // Identity tables:
        modelBuilder.Entity<CustomUser>(b =>
        {
            b.ToTable("users", identitySchemaName);
        });

        modelBuilder.Entity<CustomRole>(b =>
        {
            b.ToTable("roles", identitySchemaName);
        });

        modelBuilder.Entity<CustomUserClaim>(b =>
        {
            b.ToTable("userClaims", identitySchemaName);
        });

        modelBuilder.Entity<CustomUserRole>(b =>
        {
            b.ToTable("userRoles", identitySchemaName);
        });

        modelBuilder.Entity<CustomUserLogin>(b =>
        {
            b.ToTable("userLogins", identitySchemaName);
        });

        modelBuilder.Entity<CustomRoleClaim>(b =>
        {
            b.ToTable("roleClaims", identitySchemaName);
        });

        modelBuilder.Entity<CustomUserToken>(b =>
        {
            b.ToTable("userTokens", identitySchemaName);
        });

        modelBuilder.Entity<UserScore>(b =>
        {
            b.ToTable("UserScores", identitySchemaName);
        });

        // Conjugation models:
        modelBuilder.Entity<Conjugation>(b =>
        {
            b.ToTable("conjugations", conjugationSchemaName);
        });

        modelBuilder.Entity<Profile>(b =>
        {
            b.ToTable("profiles", conjugationSchemaName);
        });

        modelBuilder.Entity<Question>(b =>
        {
            b.ToTable("questions", conjugationSchemaName);
        });

        modelBuilder.Entity<Answer>(b =>
        {
            b.ToTable("answers", conjugationSchemaName);
        });

        // Flashcards models:
        modelBuilder.Entity<Deck>(b =>
        {
            b.ToTable("decks", flashcardsSchemaName);
        });

        modelBuilder.Entity<Unit>(b =>
        {
            b.ToTable("units", flashcardsSchemaName);
        });

        modelBuilder.Entity<Card>(b =>
        {
            b.ToTable("cards", flashcardsSchemaName);
        });

        modelBuilder.Entity<Item>(b =>
        {
            b.ToTable("items", flashcardsSchemaName);
        });

        modelBuilder.Entity<LearnStatus>(b =>
        {
            b.ToTable("learnStatuses", flashcardsSchemaName);
        });
    }
}
