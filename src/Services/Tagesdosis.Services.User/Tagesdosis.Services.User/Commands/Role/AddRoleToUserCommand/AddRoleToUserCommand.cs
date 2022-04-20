using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Commands.Role.AddRoleToUserCommand;

public class AddRoleToUserCommand : IRequest<ApiResponse>
{
    public string? UserName { get; set; }
    public string Role { get; set; }
}

public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, ApiResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public AddRoleToUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Adds the given role to the specified user
    /// </summary>
    /// <param name="request">Contains the UserName of the user whose roles should be modified</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        var result = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, request.Role));

        if (result.Succeeded)
            return new ApiResponse("Added user to role");
        
        return new ApiResponse("Invalid", result.Errors.Select(x => x.Description));
    }
}