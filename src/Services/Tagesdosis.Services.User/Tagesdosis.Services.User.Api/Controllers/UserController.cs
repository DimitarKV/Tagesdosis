using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Commands.User.UpdateUserCommand;
using Tagesdosis.Services.User.Data.Persistence;
using Tagesdosis.Services.User.DTOs;

namespace Tagesdosis.Services.User.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly UserDbContext _dbContext;

    public UserController(IMediator mediator, IMapper mapper, UserDbContext dbContext)
    {
        _mapper = mapper;
        _mediator = mediator;
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] UserCredentialsDTO credentialsDto)
    {
        var command = _mapper.Map<CreateUserCommand>(credentialsDto);
        var response = await _mediator.Send(command);

        if (response.IsValid)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public IActionResult GetAllUsersAsync()
    {
        return Ok(_dbContext.Users!.ToList());
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
    {
        var response = await _mediator.Send(updateUserCommand);

        if (response.IsValid)
            return Ok(response);
        return BadRequest(response);
    }
}