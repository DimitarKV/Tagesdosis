using Microsoft.EntityFrameworkCore;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Persistence.Interfaces;

namespace Tagesdosis.Services.Posts.Data.Persistence;

public class PostDbContext : DbContext, IPostDbContext
{

    public DbSet<Post>? Posts { get; set; }
    public DbSet<Author>? Authors { get; set; }

    public PostDbContext()
    {
    }

    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasIndex(a => a.UserName)
            .IsUnique();
        
    }
}