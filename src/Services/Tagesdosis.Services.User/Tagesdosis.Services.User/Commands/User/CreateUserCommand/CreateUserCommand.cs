using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Authorization;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Commands.User.CreateUserCommand;

public class CreateUserCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse>
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public CreateUserCommandHandler(IMapper mapper, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }
    
    /// <summary>
    /// Creates user with the given credentials 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<AppUser>(request);
        
        user.CreatedOn = DateTime.Now;
        user.UpdatedOn = DateTime.Now;
        
        var result = await _userManager.CreateAsync(user, request.Password);

        await _userManager.AddClaimAsync(user, Claims.User);
        
        if (result.Succeeded)
            return new ApiResponse("Successfully created a user");

        return new ApiResponse("An error occurred while creating a user",
            result.Errors.Select(x => x.Description));
    }
}