using Microsoft.EntityFrameworkCore;

namespace Tagesdosis.Services.Posts.Data.Persistance;

public class PostDbContext : DbContext
{
    protected PostDbContext()
    {
    }

    public PostDbContext(DbContextOptions options) : base(options)
    {
    }
}