using AutoMapper;
using Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;
using Token;

namespace Tagesdosis.Services.User.Grpc.MappingProfiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<GetTokenRequest, CreateTokenCommand>();
    }
}