using AutoMapper;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.DTOs;

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