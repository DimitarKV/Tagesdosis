using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Queries.GetRolesForUserQuery;

public class GetRolesForUserQuery : IRequest<ApiResponse<List<string>>>
{
    public string UserName { get; set; }
}

public class GetRolesForUserQueryHandler : IRequestHandler<GetRolesForUserQuery, ApiResponse<List<string>>>
{
    private readonly IIdentityService _identityService;

    public GetRolesForUserQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    /// <summary>
    /// Returns the roles of the user specified by the UserName in the request param
    /// </summary>
    /// <param name="request">Requires the UserName of the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse<List<string>>> Handle(GetRolesForUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.FindByNameAsync(request.UserName);
        var claims = await _identityService.GetClaimsAsync(user);

        var stringClaims = claims.Select(x => x.Value).ToList();

        return new ApiResponse<List<string>>(stringClaims, "Retrieved roles for user");
    }
}