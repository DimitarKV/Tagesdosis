using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Commands.User.DeleteUserCommand;
using Tagesdosis.Services.User.Commands.User.UpdateUserCommand;
using UserGrpc;

namespace Tagesdosis.Services.User.Grpc.Services;

public class UserService : UserGrpc.UserService.UserServiceBase
{
    private IMapper _mapper;
    private IMediator _mediator;
    private IHttpContextAccessor _httpContextAccessor;

    public UserService(IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<GrpcApiResponse> RegisterUser(RegisterUserRequest request,
        ServerCallContext context)
    {
        var command = _mapper.Map<CreateUserCommand>(request);
        var result = await _mediator.Send(command);

        return _mapper.Map<GrpcApiResponse>(result);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    public override async Task<GrpcApiResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        var command = _mapper.Map<UpdateUserCommand>(request);
        command.UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
        var result = await _mediator.Send(command);

        return _mapper.Map<GrpcApiResponse>(result);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    public override async Task<GrpcApiResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var command = new DeleteUserCommand
        {
            UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!
        };
        var result = await _mediator.Send(command);

        return _mapper.Map<GrpcApiResponse>(result);
    }
}