using Tagesdosis.Services.Posts.Data.Entities;

namespace Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

public interface IPostRepository
{
    public Task<Post> SavePostAsync(Post post);
    Task DeletePostAsync(int requestId);
    Task<Post?> FindByIdAsync(int id);
    Task<Post> UpdateAsync(Post post);
}