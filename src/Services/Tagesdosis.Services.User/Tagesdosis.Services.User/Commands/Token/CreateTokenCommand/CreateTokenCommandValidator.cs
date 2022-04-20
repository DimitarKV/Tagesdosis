using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator(UserManager<AppUser> userManager)
    {
        RuleFor(x => x.UserName)
            .MustAsync(async (name, _) => await userManager.FindByNameAsync(name) is not null)
            .WithErrorCode("401")
            .WithMessage("User with the given UserName does not exist in the database.");
    }
}