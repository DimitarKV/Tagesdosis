using AutoMapper;
using Grpc.Core;
using GrpcPost;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.Posts.Commands.CreatePostCommand;
using Tagesdosis.Services.Posts.Commands.DeletePostCommand;
using Tagesdosis.Services.Posts.Commands.EditPostCommand;
using Tagesdosis.Services.Posts.Queries.GetPostQuery;
using Tagesdosis.Services.Posts.Views;

namespace Tagesdosis.Services.Posts.Grpc.Services;

public class PostService : GrpcPost.GrpcPost.GrpcPostBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PostService(IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    public override async Task<CreatePostResponse> CreatePost(CreatePostRequest request, ServerCallContext context)
    {
        var command = _mapper.Map<CreatePostCommand>(request);
        command.UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
        var result = await _mediator.Send(command);

        var grpcApiResponse = _mapper.Map<GrpcApiResponse>(result);

        return new CreatePostResponse {ApiResponse = grpcApiResponse, Result = result.Result};
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    public override async Task<GetPostResponse> GetPost(GetPostRequest request, ServerCallContext context)
    {
        var query = _mapper.Map<GetPostQuery>(request);
        query.UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
        var result = await _mediator.Send(query);

        var grpcApiResponse = _mapper.Map<GrpcApiResponse>(result);
        var grpcPostView = _mapper.Map<GrpcPostView>(result.Result);

        return new GetPostResponse {ApiResponse = grpcApiResponse, PostView = grpcPostView};
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    public override async Task<UpdatePostResponse> UpdatePost(UpdatePostRequest request, ServerCallContext context)
    {
        var command = _mapper.Map<UpdatePostCommand>(request);
        command.UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
        var result = await _mediator.Send(command);

        var postResponse = new UpdatePostResponse
        {
            ApiResponse = _mapper.Map<GrpcApiResponse>(result),
            PostView = _mapper.Map<GrpcPostView>(result.Result)
        };

        return postResponse;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    public override async Task<DeletePostResponse> DeletePost(DeletePostRequest request, ServerCallContext context)
    {
        var command = new DeletePostCommand
            {Id = request.Id, UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!};

        var result = await _mediator.Send(command);

        return new DeletePostResponse {ApiResponse = _mapper.Map<GrpcApiResponse>(result)};
    }
}