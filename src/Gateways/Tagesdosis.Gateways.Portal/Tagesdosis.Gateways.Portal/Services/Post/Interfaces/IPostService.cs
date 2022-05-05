using Tagesdosis.Domain.Types;

namespace Tagesdosis.Gateways.Portal.Services.Post.Interfaces;

public interface IPostService
{
    Task<ApiResponse<int>?> CreatePostAsync(string title, string content);
}