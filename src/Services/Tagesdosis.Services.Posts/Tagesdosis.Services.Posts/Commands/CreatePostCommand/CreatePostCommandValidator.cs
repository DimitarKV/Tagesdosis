using FluentValidation;

namespace Tagesdosis.Services.Posts.Commands.CreatePostCommand;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.Title)
            .Must(t => t.Length >= 3)
            .WithErrorCode("400")
            .WithMessage("Title must be at least 3 characters long!");
    }
}