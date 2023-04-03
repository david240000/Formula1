using Microsoft.AspNetCore.Identity;

namespace Formula1.Models
{
    public class User:IdentityUser
    {
        ICollection<Rating>? ratings;
    }
}
