using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.Posts.Commands.CreatePostCommand;
using Tagesdosis.Services.Posts.DTOs;

namespace Tagesdosis.Services.Posts.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PostController : ControllerBase
{
    private IMapper _mapper;
    private IMediator _mediator;

    public PostController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO postDto)
    {
        var command = _mapper.Map<CreatePostCommand>(postDto);
        command.UserName = User.Identity!.Name!;
        var result = await _mediator.Send(command);

        if (result.IsValid)
            return Ok(result);
        return BadRequest(result);
    }
}