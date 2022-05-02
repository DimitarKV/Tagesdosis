using Microsoft.EntityFrameworkCore;
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
        var author = await _context.Authors!.FindAsync(id);
        return author;
    }

    public async Task<Author> UpdateAsync(Author author)
    {
        author = _context.Authors!.Update(author).Entity;
        await _context.SaveChangesAsync();
        return author;
    }

    public Task<Author> FindByUsernameAsync(string userName)
    {
        var author = from a in _context.Authors
            where a.UserName == userName
            select a;
        return author.Include(a => a.Posts).FirstOrDefaultAsync()!;
    }

    public async Task DeleteAuthorAsync(int id)
    {
        _context.Authors!.Remove((await FindByIdAsync(id))!);
        await _context.SaveChangesAsync();
    }
}