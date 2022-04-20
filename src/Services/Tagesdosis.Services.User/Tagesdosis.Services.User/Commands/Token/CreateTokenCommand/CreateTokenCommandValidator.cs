using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator(UserManager<AppUser> userManager)
    {
        RuleFor(x => new {x.UserName, x.Password})
            .MustAsync(async (pair, _) =>
                await userManager.CheckPasswordAsync(new AppUser { UserName = pair.UserName}, pair.Password))
            .WithErrorCode("401")
            .WithMessage("User with the given UserName does not exist in the database.");
    }
}