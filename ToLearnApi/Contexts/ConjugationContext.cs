using Microsoft.EntityFrameworkCore;
using ToLearnApi.Models.Conjugation;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.Flashcards.LearnAndReview;

namespace ToLearnApi.Contexts;

public class ConjugationContext : DbContext
{
    // Conjugation models:
    public DbSet<Conjugation> conjugations { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<ToLearnApi.Models.Conjugation.Question> questions { get; set; }
    public DbSet<ToLearnApi.Models.Conjugation.Answer> answers { get; set; }

    // Flashcards models:
    public DbSet<Deck> decks { get; set; }
    public DbSet<Unit> units { get; set; }
    public DbSet<Card> cards { get; set; }
    public DbSet<Item> items { get; set; }
    public DbSet<LearnStatus> learnStatuses { get; set; }

    public ConjugationContext(DbContextOptions<ConjugationContext> options)
        : base(options)
    {
    }
}
