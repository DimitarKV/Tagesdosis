using AutoMapper;
using Grpc.Core;
using GrpcPost;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.Posts.Commands.CreatePostCommand;
using Tagesdosis.Services.Posts.Queries.GetPostQuery;

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

    public override async Task<GetPostResponse> GetPost(GetPostRequest request, ServerCallContext context)
    {
        var query = _mapper.Map<GetPostQuery>(request);
        query.UserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
        var result = await _mediator.Send(query);

        var grpcApiResponse = _mapper.Map<GrpcApiResponse>(result);
        var grpcPostView = _mapper.Map<GrpcPostView>(result.Result);

        return new GetPostResponse {ApiResponse = grpcApiResponse, PostView = grpcPostView};
    }

    public override Task<UpdatePostResponse> UpdatePost(UpdatePostRequest request, ServerCallContext context)
    {
        return base.UpdatePost(request, context);
    }

    public override Task<DeletePostResponse> DeletePost(DeletePostRequest request, ServerCallContext context)
    {
        return base.DeletePost(request, context);
    }
}