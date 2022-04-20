using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Entities;

namespace Tagesdosis.Services.User.Data.Entities;

public class AppUser : IdentityUser, IEntity<string>
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}