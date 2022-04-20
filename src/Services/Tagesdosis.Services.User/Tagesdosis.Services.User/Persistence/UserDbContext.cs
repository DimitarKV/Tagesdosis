using Microsoft.EntityFrameworkCore;

namespace Tagesdosis.Services.User.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext()
    {
        
    }
    
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
}