using AutoMapper;
using Tagesdosis.Services.Posts.Commands.CreatePostCommand;
using Tagesdosis.Services.Posts.Commands.EditPostCommand;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.DTOs;
using Tagesdosis.Services.Posts.Views;

namespace Tagesdosis.Services.Posts.Api.MappingProfiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostDTO, CreatePostCommand>();
        CreateMap<CreatePostCommand, Post>();
        CreateMap<UpdatePostDTO, UpdatePostCommand>();
        CreateMap<Post, PostView>()
            .ForMember(d => d.Owner, o => o.MapFrom(s => s.UserName));

    }
}