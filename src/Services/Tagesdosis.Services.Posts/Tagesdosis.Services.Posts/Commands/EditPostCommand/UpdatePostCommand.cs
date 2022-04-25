using MediatR;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Commands.EditPostCommand;

public class UpdatePostCommand : IRequest<ApiResponse<Post>>
{
    public int? Id { get; set; }
    public string? UserName { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool? IsVisible { get; set; }
}

public class EditPostCommandHandler : IRequestHandler<UpdatePostCommand, ApiResponse<Post>>
{
    private readonly IPostRepository _repository;

    public EditPostCommandHandler(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<Post>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.FindByIdAsync(request.Id!.Value);

        if (request.Title is not null)
            post!.Title = request.Title;

        if (request.Content is not null)
            post!.Content = request.Content;

        if (request.IsVisible is not null)
            post!.IsVisible = request.IsVisible.Value;
        
        post.UpdatedOn = DateTime.Now;

        var persistenceResult = await _repository.UpdateAsync(post!);

        return new ApiResponse<Post>(post!, "Successfully updated post!");
    }
}