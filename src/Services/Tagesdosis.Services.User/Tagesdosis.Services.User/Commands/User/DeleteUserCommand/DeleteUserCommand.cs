using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Commands.User.DeleteUserCommand;

public class DeleteUserCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse>
{
    private UserManager<AppUser> _userManager;

    public DeleteUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        AppUser user = await _userManager.FindByNameAsync(request.UserName);
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
            return new ApiResponse("Deleted user " + request.UserName);
        return new ApiResponse("Unable to delete user with username " + request.UserName);
    }
}