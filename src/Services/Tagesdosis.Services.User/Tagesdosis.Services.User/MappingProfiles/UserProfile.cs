using AutoMapper;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Commands.User.UpdateUserCommand;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.DTOs;

namespace Tagesdosis.Services.User.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCredentialsDTO, AppUser>();
        CreateMap<AppUser, UserDTO>();   
        CreateMap<CreateUserCommand, AppUser>();
        CreateMap<UpdateUserCommand, AppUser>();
        CreateMap<UserCredentialsDTO, CreateUserCommand>();
    }
}