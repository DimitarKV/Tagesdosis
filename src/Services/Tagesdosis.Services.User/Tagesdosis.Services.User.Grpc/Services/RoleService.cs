using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Role;
using Tagesdosis.Services.User.Queries.GetRolesForUserQuery;

namespace Tagesdosis.Services.User.Grpc.Services;

public class RoleService : Role.RoleService.RoleServiceBase
{
    private IMediator _mediator;
    // private readonly HttpContextAccessor _httpContextAccessor;

    public RoleService(IMediator mediator)
    {
        _mediator = mediator;
        // _httpContextAccessor = httpContextAccessor;
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
    public override async Task<GetRolesForUserReply> GetRolesForUser(GetRolesForUserRequest request,
        ServerCallContext context)
    {
        var query = new GetRolesForUserQuery {UserName = "mitko"};
        var response = await _mediator.Send(query);
    
        if (response.IsValid)
            return new GetRolesForUserReply
            {
                Roles = {response.Result}
            };

        return new GetRolesForUserReply();
    }
}