using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Commands.Role.AddUserToRoleCommand;

public class AddUserToRoleCommand : IRequest<ApiResponse>
{
    public string? UserName { get; set; }
    public string Role { get; set; }
}

public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand, ApiResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public AddUserToRoleCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        var result = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, request.Role));

        if (result.Succeeded)
            return new ApiResponse("Added user to role");
        
        return new ApiResponse("Invalid", result.Errors.Select(x => x.Description));
    }
}