using Tagesdosis.Domain.Types;

namespace Tagesdosis.Gateways.Portal.Services.User;

public interface IUserService
{
    Task<ApiResponse?> CreateAsync(string userName, string email, string password);
    Task<ApiResponse<string>?> CheckPasswordAsync(string userName, string password);
}