using FluentValidation;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.User.UpdateUserCommand;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IIdentityService identityService)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (u, _) =>
            {
                var user = await identityService.FindByNameAsync(u);
                if (user is null)
                    return false;
                return true;
            })
            .WithMessage("The specified user doesn't exist!")
            .WithErrorCode("404");
    }
}