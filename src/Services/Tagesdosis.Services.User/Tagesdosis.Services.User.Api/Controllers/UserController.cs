using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Commands.User.DeleteUserCommand;
using Tagesdosis.Services.User.Commands.User.UpdateUserCommand;
using Tagesdosis.Services.User.DTOs;

namespace Tagesdosis.Services.User.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private IMapper _mapper;
    private readonly IMediator _mediator;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.IsValid)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDto)
    {
        var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
        command.UserName = User.Identity!.Name!;
        var response = await _mediator.Send(command);

        if (response.IsValid)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeleteUser()
    {
        var command = new DeleteUserCommand();
        command.UserName = User.Identity!.Name;
        
        var response = await _mediator.Send(command);

        if (response.IsValid)
            return Ok(response);
        return BadRequest(response);
    }
}