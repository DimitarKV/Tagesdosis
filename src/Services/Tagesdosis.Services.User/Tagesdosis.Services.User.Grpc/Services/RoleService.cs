using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Role;
using Tagesdosis.Services.User.Commands.Role.AddRoleToUserCommand;
using Tagesdosis.Services.User.Identity;
using Tagesdosis.Services.User.Queries.Role.GetRolesForUser;

namespace Tagesdosis.Services.User.Grpc.Services;

public class RoleService : Role.RoleService.RoleServiceBase
{
    private IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private IIdentityService _identityService;
    private IMapper _mapper;

    public RoleService(IMediator mediator, IHttpContextAccessor httpContextAccessor, IIdentityService identityService, IMapper mapper)
    {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
        _mapper = mapper;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    public override async Task<GrpcApiResponseStrings> GetRolesForUser(GetRolesForUserRequest request,
        ServerCallContext context)
    {
        var query = new GetRolesForUserQuery {UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!};
        var result = await _mediator.Send(query);

        return _mapper.Map<GrpcApiResponseStrings>(result);
    }

    public override async Task<GrpcApiResponse> AddRoleToUser(AddRoleToUserRequest request,
        ServerCallContext context)
    {
        var command = _mapper.Map<AddRoleToUserCommand>(request);
        var result = await _mediator.Send(command);

        return _mapper.Map<GrpcApiResponse>(result);
    }
}