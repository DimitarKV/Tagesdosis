using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Commands.Role.AddRoleToUserCommand;

public class AddRoleToUserCommandValidator : AbstractValidator<AddRoleToUserCommand>
{
    public AddRoleToUserCommandValidator(IIdentityService identityService)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (userName, _) => await identityService.FindByNameAsync(userName) is not null)
            .WithErrorCode("404")
            .WithMessage("The given UserName does not exist in the database");

        RuleFor(cmd => cmd.Role)
            .NotEmpty()
            .WithMessage("The role must not be empty");
    }
}