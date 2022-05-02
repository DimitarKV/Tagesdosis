using Microsoft.EntityFrameworkCore;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Persistence.Interfaces;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Data.Repositories;

public class PostRepository : IPostRepository
{
    private IPostDbContext _context;

    public PostRepository(IPostDbContext context)
    {
        _context = context;
    }

    public async Task<Post> SavePostAsync(Post post)
    {
        var result = _context.Posts!.Add(post);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Post?> FindByIdAsync(int id)
    {
        var post = await _context.Posts!.Include(p => p.Author).Where(p => p.Id == id).FirstOrDefaultAsync();
        return post;
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        post = _context.Posts!.Update(post).Entity;
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task DeletePostAsync(int id)
    {
        _context.Posts!.Remove((await FindByIdAsync(id))!);
        await _context.SaveChangesAsync();
    }
}