using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Queries.User.GetUserQuery;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator(UserManager<AppUser> userManager)
    {
        RuleFor(query => query.UserName)
            .MustAsync(async (userName, _) => await userManager.FindByNameAsync(userName) is not null)
            .WithErrorCode("404")
            .WithMessage("The given UserName does not exist in the database");
    }
}