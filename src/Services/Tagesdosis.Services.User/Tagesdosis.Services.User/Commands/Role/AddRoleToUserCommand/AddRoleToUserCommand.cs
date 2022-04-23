using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.Role.AddRoleToUserCommand;

public class AddRoleToUserCommand : IRequest<ApiResponse>
{
    public string? UserName { get; set; }
    public string Role { get; set; }

    public AddRoleToUserCommand()
    {
        
    }

    public AddRoleToUserCommand(string userName, string role)
    {
        UserName = userName;
        Role = role;
    }
}

public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, ApiResponse>
{
    private readonly IIdentityService _identityService;

    public AddRoleToUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    /// <summary>
    /// Adds the given role to the specified user
    /// </summary>
    /// <param name="request">Contains the UserName of the user whose roles should be modified</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.FindByNameAsync(request.UserName);
        var result = await _identityService.AddClaimAsync(user, new Claim(ClaimTypes.Role, request.Role));

        if (result.Succeeded)
            return new ApiResponse("Added user to role");
        
        return new ApiResponse("Invalid", result.Errors.Select(x => x.Description));
    }
}