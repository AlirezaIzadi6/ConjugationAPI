using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using ToLearnApi.Models.Identity;

namespace ToLearnApi.Models.Conjugation;

public class Profile
{
    public int Id { get; set; }
    [ForeignKey(nameof(CustomUser))]
    public string UserId { get; set; }
    public CustomUser User { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(8000)]
    public string Moods { get; set; } = string.Empty;
    [StringLength(1000)]
    public string Infinitives {  get; set; } = string.Empty;
    public string Persons {  get; set; } = string.Empty;

    public ProfileDto GetDto()
    {
        return new ProfileDto()
        {
            Id = Id,
            Name = Name,
            Moods = Moods,
            Infinitives = Infinitives,
            Persons = Persons
        };
    }

    public void UpdateWithDto(ProfileDto dto)
    {
        this.Id = dto.Id;
        this.Name = dto.Name;
        this.Moods = dto.Moods;
        this.Infinitives = dto.Infinitives;
        this.Persons = dto.Persons;
    }

    public bool CheckUser(ClaimsPrincipal user)
    {
        if (this.UserId == user.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return true;
        }
        return false;
    }
}
