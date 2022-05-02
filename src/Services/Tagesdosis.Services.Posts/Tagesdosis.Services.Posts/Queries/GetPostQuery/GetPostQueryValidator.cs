using FluentValidation;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Queries.GetPostQuery;

public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
{
    public GetPostQueryValidator(IPostRepository postRepository)
    {
        RuleFor(c => c.Id)
            .MustAsync(async (id, _) =>
            {
                var post = await postRepository.FindByIdAsync(id);
                if (post is null)
                    return false;
                return true;
            })
            .WithMessage("No such post exists in the database!")
            .WithErrorCode("404");
        
        RuleFor(c => c)
            .MustAsync(async (q, _) =>
            {
                
                var post = await postRepository.FindByIdAsync(q.Id);
                if (post is null)
                    return true;
                if (!post.IsVisible && post.Author.UserName != q.UserName)
                    return false;
                return true;
            })
            .WithMessage("You must be the owner of an invisible post in order to see it!")
            .WithErrorCode("401");
    }
}