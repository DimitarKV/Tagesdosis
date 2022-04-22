using FluentValidation;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.User.CreateUserCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IIdentityService identityService)
    {
        RuleFor(command => command.UserName)
            .MustAsync(async (userName, _) =>
                await identityService.FindByNameAsync(userName) is null)
            .WithErrorCode("400")
            .WithMessage("User already exists");
    }
}