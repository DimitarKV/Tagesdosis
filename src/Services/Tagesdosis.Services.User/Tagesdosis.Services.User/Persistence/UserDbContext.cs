using Microsoft.EntityFrameworkCore;
using Tagesdosis.Services.User.Entities;

namespace Tagesdosis.Services.User.Persistence;

public class UserDbContext : DbContext
{
    public DbSet<AppUser>? AppUsers { get; set; }
    
    public UserDbContext()
    {
        
    }
    
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
}