using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Entities;

namespace Tagesdosis.Services.User.Entities;

public class AppUser : IdentityUser, IEntity<string>
{
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}