using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.User.UpdateUserCommand;

public class UpdateUserCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; }
    public string NewUserName { get; set; }
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IMapper mapper, IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    /// <summary>
    /// Updates the fields of the user which are not empty strings
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.FindByNameAsync(request.UserName);
            bool updated = false;
        if (user is null)
            return new ApiResponse("Didn't find user with username " + request.UserName);
        if (request.UserName != "")
        {
            user.UserName = request.UserName;
            updated = true;
        }

        if (request.Email != "")
        {
            user.Email = request.Email;
            updated = true;
        }

        IdentityResult result = new IdentityResult();
        if (request.NewPassword != "")
        {
            result = await _identityService.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return new ApiResponse("Couldn't change password of user " + request.UserName);
            updated = true;
        }

        if (updated)
            user.UpdatedOn = DateTime.Now;

        result = await _identityService.UpdateAsync(user);
        
        if (result.Succeeded)
            return new ApiResponse("Successfully updated user " + request.UserName);

        return new ApiResponse("An error occurred while creating a user",
            result.Errors.Select(x => x.Description));
    }
}