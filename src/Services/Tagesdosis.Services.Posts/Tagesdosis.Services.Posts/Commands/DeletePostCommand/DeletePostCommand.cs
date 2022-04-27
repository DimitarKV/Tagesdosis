using MediatR;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Commands.DeletePostCommand;

public class DeletePostCommand : IRequest<ApiResponse>
{
    public int Id { get; set; }
    public string UserName { get; set; }

    public DeletePostCommand()
    {
    }

    public DeletePostCommand(int id, string userName)
    {
        Id = id;
        UserName = userName;
    }
}

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ApiResponse>
{
    private IPostRepository _repository;

    public DeletePostCommandHandler(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeletePostAsync(request.Id);

        return new ApiResponse("Successfully deleted post with id: " + request.Id);
    }
}