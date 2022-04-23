using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.User.UpdateUserCommand;

public class UpdateUserCommand : IRequest<ApiResponse>
{
    public bool ChangeUsername { get; set; }
    public string UserName { get; set; }
    public string NewUserName { get; set; }
    public bool ChangeEmail { get; set; }
    public string Email { get; set; }
    public bool ChangePassword { get; set; }
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
        
        if (user is null)
            return new ApiResponse("Didn't find user with username " + request.UserName).SetInvalid();
        
        if (request.ChangeUsername)
            user.UserName = request.NewUserName;
        
        if (request.ChangeEmail)
            user.Email = request.Email;

        IdentityResult result = new IdentityResult();
        if (request.ChangePassword)
        {
            result = await _identityService.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return new ApiResponse("Couldn't change password of user " + request.UserName, result.Errors.Select(e => e.Description));
        }

        if (request.ChangeEmail || request.ChangePassword || request.ChangeUsername)
            user.UpdatedOn = DateTime.Now;

        result = await _identityService.UpdateAsync(user);

        string messageIfPasswordChanged = "";
        if (request.ChangeUsername)
            messageIfPasswordChanged = " Please login again with your new username " + request.NewUserName;
        
        if (result.Succeeded)
            return new ApiResponse("Successfully updated user " + request.UserName + "." + messageIfPasswordChanged);

        return new ApiResponse("An error occurred while creating a user",
            result.Errors.Select(x => x.Description));
    }
}