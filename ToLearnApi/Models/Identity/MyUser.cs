using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ToLearnApi.Models.Conjugation;
using ToLearnApi.Models.Flashcards;
using ToLearnApi.Models.Flashcards.LearnAndReview;

namespace ToLearnApi.Models.Identity;

public class CustomUser : IdentityUser
{
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public int Score { get; set; } = 0;
    public List<Card> Cards { get; set; } = new();
    public List<Unit> Units { get; set; } = new();
    public List<Deck> Decks { get; set; } = new();
    public List<Item> Items { get; set; } = new();
    public List<LearnStatus> learnStatuses { get; set; } = new();
    public List<Profile> Profiles { get; set; } = new();
    public List<Question> Questions { get; set; } = new();
    public List<UserScore> UserScores { get; set; } = new();
}

public class CustomRole : IdentityRole
{
}

public class CustomUserClaim : IdentityUserClaim<string>
{
}

public class CustomUserRole : IdentityUserRole<string>
{
}

public class CustomUserLogin : IdentityUserLogin<string>
{
}

public class CustomRoleClaim : IdentityRoleClaim<string>
{
}

public class CustomUserToken : IdentityUserToken<string>
{
}
