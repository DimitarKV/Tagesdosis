using AutoMapper;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Commands.User.UpdateUserCommand;
using UserGrpc;

namespace Tagesdosis.Services.User.Grpc.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserRequest, CreateUserCommand>();
        CreateMap<ApiResponse, GrpcApiResponse>();
        CreateMap<UpdateUserRequest, UpdateUserCommand>();
    }
}