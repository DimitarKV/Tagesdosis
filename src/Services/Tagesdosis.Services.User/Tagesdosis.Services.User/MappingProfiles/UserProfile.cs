using AutoMapper;
using Tagesdosis.Services.User.DTOs;
using Tagesdosis.Services.User.Entities;

namespace Tagesdosis.Services.User.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCredentialsDTO, AppUser>();
    }
}