using AutoMapper;
using MediatR;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Commands.CreatePostCommand;

public class CreatePostCommand : IRequest<ApiResponse<int>>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
    public bool IsVisible { get; set; }

    public CreatePostCommand()
    {
    }

    public CreatePostCommand(string title, string content, string userName, bool isVisible)
    {
        Title = title;
        Content = content;
        UserName = userName;
        IsVisible = isVisible;
    }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ApiResponse<int>>
{
    private IPostRepository _repository;
    private IMapper _mapper;

    public CreatePostCommandHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<int>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<Post>(request);
        post.CreatedOn = DateTime.Now;
        post.UpdatedOn = DateTime.Now;

        var id = await _repository.SavePostAsync(post);
        
        return new ApiResponse<int>(id, "Successfully created post!");
    }
}