using AutoMapper;
using Tagesdosis.Services.User.Api.DTOs;
using Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;

namespace Tagesdosis.Services.User.MappingProfiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<UserCredentialsDTO, CreateTokenCommand>();
    }
}