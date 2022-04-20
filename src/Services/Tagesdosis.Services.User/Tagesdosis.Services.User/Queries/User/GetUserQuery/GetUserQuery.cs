using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.DTOs;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Queries.User.GetUserQuery;

public class GetUserQuery : IRequest<ApiResponse<UserDTO>>
{
    public string UserName { get; set; }
}


public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ApiResponse<UserDTO>>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Retrieves user from db by username
    /// </summary>
    /// <param name="request">Contains the username of the user</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApiResponse<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.FindByNameAsync(request.UserName);
        var dto = _mapper.Map<UserDTO>(user);

        return new ApiResponse<UserDTO>(dto, "Retrieved user");
    }
}