using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Persistence;
using Tagesdosis.Services.Posts.Data.Persistence.Interfaces;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Data.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly PostDbContext _context;

    public AuthorRepository(PostDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAuthorAsync(Author author)
    {
        var entry = _context.Authors!.Add(author);
        await _context.SaveChangesAsync();

        return entry.Entity.Id;
    }

    public async Task<Author?> FindByIdAsync(int id)
    {
        var post = await _context.Authors!.FindAsync(id);
        return post;
    }

    public Task<Author> UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public async Task<Author> UpdateAsync(Author author)
    {
        author = _context.Authors!.Update(author).Entity;
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task DeleteAuthorAsync(int id)
    {
        _context.Authors!.Remove((await FindByIdAsync(id))!);
        await _context.SaveChangesAsync();
    }
}