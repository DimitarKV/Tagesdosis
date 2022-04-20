using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.DTOs;
using Tagesdosis.Services.User.Entities;
using Tagesdosis.Services.User.Persistence;

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
    [Authorize]
    public IActionResult GetAllUsersAsync()
    {
        return Ok(_dbContext.AppUsers!.ToList());
    }
}