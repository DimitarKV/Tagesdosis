using Tagesdosis.Services.Posts.Data.Entities;

namespace Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

public interface IAuthorRepository
{
    public Task<int> SaveAuthorAsync(Author author);
    Task DeleteAuthorAsync(int requestId);
    Task<Author?> FindByIdAsync(int id);
    Task<Author> UpdateAsync(Author author);
    Task<Author> FindByUsernameAsync(string userName);
}