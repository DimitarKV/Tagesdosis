using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Data.Persistence;

public class UserDbContext : IdentityDbContext<AppUser>
{
    public UserDbContext()
    {
        
    }
    
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
}