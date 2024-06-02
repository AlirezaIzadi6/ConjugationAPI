using System.ComponentModel.DataAnnotations;

namespace ToLearnApi.Models.Conjugation;

public class ProfileDto
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(8000)]
    public string Infinitives { get; set; } = string.Empty;
    [StringLength(1000)]
    public string Moods { get; set; } = string.Empty;
    public string Persons { get; set; } = string.Empty;

    public Profile GetProfile(string userId)
    {
        return new Profile()
        {
            UserId = userId,
            Name = Name,
            Moods = Moods,
            Infinitives = Infinitives,
            Persons = Persons
        };
    }
}
