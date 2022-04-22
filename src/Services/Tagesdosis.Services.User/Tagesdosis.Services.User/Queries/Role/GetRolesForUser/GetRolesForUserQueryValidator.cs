using FluentValidation;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Queries.Role.GetRolesForUser;

public class GetRolesForUserQueryValidator : AbstractValidator<GetRolesForUserQuery>
{
    public GetRolesForUserQueryValidator(IIdentityService identityService)
    {
        RuleFor(cmd => cmd.UserName)
            .MustAsync(async (userName, _) => await identityService.FindByNameAsync(userName) is not null)
            .WithErrorCode("404")
            .WithMessage("The given UserName does not exist in the database");
    }
}