using System.Security.Claims;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Role;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Identity;
using Tagesdosis.Services.User.Queries.Role.GetRolesForUser;

namespace Tagesdosis.Services.User.Grpc.Services;

public class RoleService : Role.RoleService.RoleServiceBase
{
    private IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private IIdentityService _identityService;

    public RoleService(IMediator mediator, IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
    {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
    public override async Task<GetRolesForUserReply> GetRolesForUser(GetRolesForUserRequest request,
        ServerCallContext context)
    {
        var query = new GetRolesForUserQuery {UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!};
        var response = await _mediator.Send(query);

        if (response.IsValid)
            return new GetRolesForUserReply
            {
                Roles = {response.Result}
            };

        return new GetRolesForUserReply();
    }

    public override async Task<AddRoleToUserResponse> AddRoleToUser(AddRoleToUserRequest request,
        ServerCallContext context)
    {
        var user = await _identityService.FindByNameAsync(request.UserName);
        var result = await _identityService.AddClaimAsync(user, new Claim(ClaimTypes.Role, request.Role));

        if (result.Succeeded)
            return new AddRoleToUserResponse {Message = "Added role to user"};

        return new AddRoleToUserResponse {Message = "Invalid " + result.Errors.Select(x => x.Description)};
    }
}