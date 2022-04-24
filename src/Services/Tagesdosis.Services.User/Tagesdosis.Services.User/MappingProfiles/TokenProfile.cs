using AutoMapper;
using Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;
using Tagesdosis.Services.User.DTOs;

namespace Tagesdosis.Services.User.MappingProfiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<UserCredentialsDTO, CreateTokenCommand>();
    }
}