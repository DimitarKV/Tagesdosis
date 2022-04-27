using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Persistance.Interfaces;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Data.Repositories;

public class PostRepository : IPostRepository
{
    private IPostDbContext _context;

    public PostRepository(IPostDbContext context)
    {
        _context = context;
    }

    public async Task<int> SavePostAsync(Post post)
    {
        var entry = _context.Posts!.Add(post);
        await _context.SaveChangesAsync();

        return entry.Entity.Id;
    }

    public async Task<Post> FindByIdAsync(int id)
    {
        Post post = await _context.Posts!.FindAsync(id);
        return post;
    }

    public async Task DeletePostAsync(int id)
    {
        _context.Posts.Remove(await FindByIdAsync(id));
        _context.SaveChanges();
    }
}