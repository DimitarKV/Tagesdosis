using Tagesdosis.Application.Infrastructure.ServiceConsumers;
using Tagesdosis.Domain.Types;
using Tagesdosis.Gateways.Portal.Static;

namespace Tagesdosis.Gateways.Portal.Services.User;

[BaseUrl("Services:User:Client", LoadFromConfiguration = true)]
public partial class UserServiceConsumer : ServiceConsumerBase
{
    [Action(Endpoints.CreateUser, "Post")]
    [ContentType("application/json")]
    public partial Task<ApiResponse> CreateAsync(CreateUserModel model);
    
    [Action(Endpoints.CheckPassword, "Post")]
    [ContentType("application/json")]
    public partial Task<ApiResponse<string>> CheckPasswordAsync(CheckPasswordModel model);
}