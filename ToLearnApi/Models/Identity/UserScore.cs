using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToLearnApi.Models.Identity;

public class UserScore
{
    public int Id { get; set; }
    [ForeignKey(nameof(CustomUser))]
    public string UserId { get; set; }
    public CustomUser User { get; set; }
    public int Score { get; set; }
    public string Reason { get; set; }
    public DateTime TimeStamp { get; set; }
}
