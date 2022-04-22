using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;

namespace Tagesdosis.Services.User.Queries.User.GetUserQuery;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    /// <summary>
    /// Validator that checks whether the user exists in the database
    /// </summary>
    public GetUserQueryValidator(IIdentityService identityService)
    {
        RuleFor(query => query.UserName)
            .MustAsync(async (userName, _) => await identityService.FindByNameAsync(userName) is not null)
            .WithErrorCode("404")
            .WithMessage("The given UserName does not exist in the database");
    }
}