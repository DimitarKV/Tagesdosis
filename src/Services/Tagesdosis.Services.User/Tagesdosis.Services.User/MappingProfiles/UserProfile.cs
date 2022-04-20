using AutoMapper;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.DTOs;
using Tagesdosis.Services.User.Entities;

namespace Tagesdosis.Services.User.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCredentialsDTO, AppUser>();
        
        CreateMap<CreateUserCommand, AppUser>();
        CreateMap<UserCredentialsDTO, CreateUserCommand>();
    }
}