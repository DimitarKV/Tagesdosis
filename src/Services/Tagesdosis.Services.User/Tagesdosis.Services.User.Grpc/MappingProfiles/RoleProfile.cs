using AutoMapper;
using Role;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Commands.Role.AddRoleToUserCommand;

namespace Tagesdosis.Services.User.Grpc.MappingProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<AddRoleToUserRequest, AddRoleToUserCommand>();
        CreateMap<ApiResponse<List<string>>, GrpcApiResponseStrings>();
        CreateMap<ApiResponse, GrpcApiResponse>();
    }
}