namespace ToLearnApi.Models.Conjugation;

public class ProfileDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Moods { get; set; }
    public string Infinitives { get; set; }
    public string Persons {  get; set; }

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
