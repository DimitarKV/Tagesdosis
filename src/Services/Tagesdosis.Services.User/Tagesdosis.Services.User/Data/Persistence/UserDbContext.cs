using Microsoft.EntityFrameworkCore;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Data.Persistence;

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