using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MediatR;
using Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;
using Token;

namespace Tagesdosis.Services.User.Grpc.Services;

public class TokenService : Token.TokenService.TokenServiceBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public TokenService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<ApiResponseString> GetToken(GetTokenRequest request, ServerCallContext context)
    {
        var command = _mapper.Map<CreateTokenCommand>(request);
        var result = await _mediator.Send(command);

        return _mapper.Map<ApiResponseString>(result);
    }
}