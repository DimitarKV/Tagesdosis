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
                await userManager.CheckPasswordAsync(await userManager.FindByNameAsync(pair.UserName), pair.Password))
            .WithErrorCode("401")
            .WithMessage("Wrong password");
    }
}