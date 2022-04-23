using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.User.DeleteUserCommand;

public class DeleteUserCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; }

    public DeleteUserCommand()
    {
        
    }

    public DeleteUserCommand(string userName)
    {
        UserName = userName;
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse>
{
    private readonly IIdentityService _identityService;

    public DeleteUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    /// <summary>
    /// Deletes the user with the given UserName
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.FindByNameAsync(request.UserName);
        var result = await _identityService.DeleteAsync(user!);
        if (result.Succeeded)
            return new ApiResponse("Deleted user " + request.UserName);
        return new ApiResponse("Unable to delete user with username " + request.UserName);
    }
}