using Tagesdosis.Application.Infrastructure.ServiceConsumers;
using Tagesdosis.Domain.Types;

namespace Tagesdosis.Gateways.Portal.Services.User;

[BaseUrl("Services:User:Client", LoadFromConfiguration = true)]
public partial class UserServiceConsumer : IServiceConsumer
{
    [Action("/api/user", "Post")]
    [ContentType("application/json")]
    public partial Task<ApiResponse> CreateUser(CreateUserModel model);
}