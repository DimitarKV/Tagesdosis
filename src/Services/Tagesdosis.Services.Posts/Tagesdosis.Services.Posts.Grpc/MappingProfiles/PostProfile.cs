using AutoMapper;
using GrpcPost;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.Posts.Commands.CreatePostCommand;
using Tagesdosis.Services.Posts.Commands.EditPostCommand;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Queries.GetPostQuery;
using Tagesdosis.Services.Posts.Views;

namespace Tagesdosis.Services.Posts.Grpc.MappingProfiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostRequest, CreatePostCommand>();
        CreateMap<ApiResponse<int>, GrpcApiResponse>();
        CreateMap<GetPostRequest, GetPostQuery>();
        CreateMap<CreatePostCommand, Post>();
        CreateMap<Post, PostView>()
            .ForMember(d => d.Owner, o => o.MapFrom(s => s.UserName));
        CreateMap<ApiResponse<PostView>, GrpcApiResponse>();
        CreateMap<PostView, GrpcPostView>();
        CreateMap<ApiResponse<PostView>, GrpcApiResponse>();
        CreateMap<PostView, GrpcPostView>();
        CreateMap<UpdatePostRequest, UpdatePostCommand>();
        CreateMap<ApiResponse, GrpcApiResponse>();
    }
}