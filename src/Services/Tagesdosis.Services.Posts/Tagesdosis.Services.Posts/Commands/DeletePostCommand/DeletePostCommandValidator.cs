using FluentValidation;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Commands.DeletePostCommand;


public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator(IPostRepository postRepository)
    {
        RuleFor(c => c.Id)
            .MustAsync(async (id, _) => (await postRepository.FindByIdAsync(id)) is not null)
            .WithErrorCode("404")
            .WithMessage("No such post exists in the database!");
        
        RuleFor(c => c)
            .MustAsync(async (cmd, _) =>
            {
                var post = await postRepository.FindByIdAsync(cmd.Id);
                if (post is null)
                    return true;
                return post.Author.UserName == cmd.UserName;
            })
            .WithErrorCode("403")
            .WithMessage("The specified post is not owned by the specified user!");

    }
}