using FluentValidation;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Commands.EditPostCommand;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator(IPostRepository postRepository)
    {
        
        RuleFor(cmd => cmd.Id)
            .NotNull()
            .WithMessage("Id is required!")
            .WithErrorCode("400");

        RuleFor(cmd => cmd.UserName)
            .NotNull()
            .WithMessage("UserName is required!")
            .WithErrorCode("400");

        RuleFor(cmd => cmd.Title)
            .Must(t =>
            {
                if (t is null)
                    return true;
                if (t.Length < 3)
                    return false;
                return true;
            })
            .WithMessage("Title length must be at least 3 characters!")
            .WithErrorCode("400");

        RuleFor(cmd => cmd)
            .MustAsync(async (cmd, _) =>
            {
                if (cmd.Id is null)
                    return true;
                if (cmd.UserName is null)
                    return true;

                var post = await postRepository.FindByIdAsync(cmd.Id.Value);
                
                if (post is null)
                    return false;
                
                return true;
            })
            .WithMessage("Post with the specified Id doesn't exist!")
            .WithErrorCode("404");
        RuleFor(cmd => cmd)
            .MustAsync(async (cmd, _) =>
            {
                if (cmd.Id is null)
                    return true;
                if (cmd.UserName is null)
                    return true;

                var post = await postRepository.FindByIdAsync(cmd.Id.Value);
                if (post is null)
                    return true;

                if (post.UserName != cmd.UserName)
                    return false;
                    
                return true;
            })
            .WithMessage("The post is not owned by the specified user!")
            .WithErrorCode("401");
    }
}