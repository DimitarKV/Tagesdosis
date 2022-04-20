using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Entities;

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
    
    public async Task<ApiResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<AppUser>(request);
        
        user.CreatedDateTime = DateTime.Now;
        user.UpdatedDateTime = DateTime.Now;
        
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
            return new ApiResponse();

        return new ApiResponse(result.Errors.Select(x => x.Description));
    }
}