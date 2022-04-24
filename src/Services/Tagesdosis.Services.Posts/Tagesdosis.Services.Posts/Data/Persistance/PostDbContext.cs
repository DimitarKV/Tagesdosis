using Microsoft.EntityFrameworkCore;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Persistance.Interfaces;

namespace Tagesdosis.Services.Posts.Data.Persistance;

public class PostDbContext : DbContext, IPostDbContext
{

    public DbSet<Post>? Posts { get; set; }

    public PostDbContext()
    {
    }

    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
    {
    }

}