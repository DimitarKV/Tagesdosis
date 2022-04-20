using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.User.Queries.GetRolesForUserQuery;

namespace Tagesdosis.Services.User.Grpc.Services;

public class RoleService : Grpc.RoleService.RoleServiceBase
{
    private IMediator _mediator;
    private readonly HttpContextAccessor _httpContextAccessor;

    public RoleService(IMediator mediator,  HttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<GetRolesForUserReply> GetRolesForUserAsync()
    {
        var query = new GetRolesForUserQuery {UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!};
        var response = await _mediator.Send(query);
    
        if (response.IsValid)
            return new GetRolesForUserReply
            {
                Roles = { response.Result }
            };
        
        return new GetRolesForUserReply();
    }
}