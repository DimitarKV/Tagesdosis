using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;
using Tagesdosis.Services.User.DTOs;

namespace Tagesdosis.Services.User.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public TokenController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetToken([FromBody] UserCredentialsDTO credentialsDto)
    {
        var command = _mapper.Map<CreateTokenCommand>(credentialsDto);
        var response = await _mediator.Send(command);

        if (response.IsValid)
            return Ok(response);
        
        return Unauthorized(response);
    }
}