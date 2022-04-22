using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator(IIdentityService identityService)
    {
        RuleFor(x => new {x.UserName, x.Password})
            .MustAsync(async (pair, _) =>
                await identityService.CheckPasswordAsync(await identityService.FindByNameAsync(pair.UserName), pair.Password))
            .WithErrorCode("401")
            .WithMessage("Wrong password");
    }
}