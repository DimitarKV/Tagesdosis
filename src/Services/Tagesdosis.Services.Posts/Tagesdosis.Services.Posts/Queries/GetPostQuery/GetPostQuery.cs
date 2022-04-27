using AutoMapper;
using MediatR;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;
using Tagesdosis.Services.Posts.Views;

namespace Tagesdosis.Services.Posts.Queries.GetPostQuery;

public class GetPostQuery : IRequest<ApiResponse<PostView>>
{
    public int Id { get; set; }
    public string UserName { get; set; }
}

public class GerPostQueryHandler : IRequestHandler<GetPostQuery, ApiResponse<PostView>>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public GerPostQueryHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PostView>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _repository.FindByIdAsync(request.Id);
        return new ApiResponse<PostView>(_mapper.Map<PostView>(post), "Success!");
    }
}