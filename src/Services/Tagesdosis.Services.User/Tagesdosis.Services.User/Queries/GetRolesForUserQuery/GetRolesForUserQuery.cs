using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Queries.GetRolesForUserQuery;

public class GetRolesForUserQuery : IRequest<ApiResponse<List<string>>>
{
    public string UserName { get; set; }
}

public class GetRolesForUserQueryHandler : IRequestHandler<GetRolesForUserQuery, ApiResponse<List<string>>>
{
    private readonly UserManager<AppUser> _userManager;

    public GetRolesForUserQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
        
    /// <summary>
    /// Handler for role retrievement
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse<List<string>>> Handle(GetRolesForUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        var claims = await _userManager.GetClaimsAsync(user);

        var stringClaims = claims.Select(x => x.Value).ToList();

        return new ApiResponse<List<string>>(stringClaims, "Retrieved roles for user");
    }
}