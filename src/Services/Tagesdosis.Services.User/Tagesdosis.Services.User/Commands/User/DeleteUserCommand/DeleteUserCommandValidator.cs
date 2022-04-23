using FluentValidation;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.User.DeleteUserCommand;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(IIdentityService identityService)
    {
        RuleFor(command => command.UserName)
            .MustAsync(async (userName, _) =>
                await identityService.FindByNameAsync(userName) is not null)
            .WithErrorCode("404")
            .WithMessage("Not found");
    }
}