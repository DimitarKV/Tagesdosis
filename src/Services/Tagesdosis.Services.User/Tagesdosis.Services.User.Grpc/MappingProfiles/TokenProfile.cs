using AutoMapper;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;
using Token;

namespace Tagesdosis.Services.User.Grpc.MappingProfiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<GetTokenRequest, CreateTokenCommand>();
        CreateMap<ApiResponse<string>, ApiResponseString>()
            .ForMember(dest => dest.Result, 
                opt => opt
                    .MapFrom(v => v.Result == null ? "" : v.Result));
    }
}