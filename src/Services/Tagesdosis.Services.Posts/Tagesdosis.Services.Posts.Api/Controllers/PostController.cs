﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.Posts.Commands.CreatePostCommand;
using Tagesdosis.Services.Posts.Commands.DeletePostCommand;
using Tagesdosis.Services.Posts.Commands.EditPostCommand;
using Tagesdosis.Services.Posts.DTOs;
using Tagesdosis.Services.Posts.Queries.GetPostQuery;

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

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetPost(int id)
    {
        var query = new GetPostQuery() {Id = id, UserName = User.Identity!.Name!};
        var result = await _mediator.Send(query);
        
        if(result.IsValid)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdatePost([FromBody] UpdatePostDTO updatePostDto)
    {
        var command = _mapper.Map<UpdatePostCommand>(updatePostDto);
        command.UserName = User.Identity!.Name!;
        var result = await _mediator.Send(command);

        if (result.IsValid)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var command = new DeletePostCommand {Id = id, UserName = User.Identity!.Name!};
        var result = await _mediator.Send(command);

        if (result.IsValid)
            return Ok(result);
        return BadRequest(result);
    }
}