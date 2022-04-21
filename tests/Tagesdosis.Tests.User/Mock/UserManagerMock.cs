using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Tests.User.Mock;

public static class UserManagerMock
{
    public static UserManager<AppUser> Create()
    {
        var userPasswordStore = new Mock<IUserPasswordStore<AppUser>>();
        userPasswordStore.Setup(s => s.CreateAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(IdentityResult.Success));
        userPasswordStore.Setup(s => s.UpdateAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(IdentityResult.Success));
        userPasswordStore.Setup(s => s.DeleteAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(IdentityResult.Success));
        userPasswordStore.Setup(s => s.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new AppUser()));
        
        var claimStore = userPasswordStore.As<IUserClaimStore<AppUser>>();
        claimStore.Setup(s =>
                s.AddClaimsAsync(It.IsAny<AppUser>(), It.IsAny<IEnumerable<Claim>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(IdentityResult.Success));
        claimStore.Setup(s =>
                s.GetClaimsAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new Mock<IList<Claim>>().Object));
        
        var options = new Mock<IOptions<IdentityOptions>>();
        var idOptions = new IdentityOptions();

        //sync with settings in ConfigureIdentity in WebApi -> Startup.cs
        idOptions.Lockout.AllowedForNewUsers = false;
        idOptions.Password.RequireDigit = true;
        idOptions.Password.RequireLowercase = true;
        idOptions.Password.RequireNonAlphanumeric = true;
        idOptions.Password.RequireUppercase = true;
        idOptions.Password.RequiredLength = 8;
        idOptions.Password.RequiredUniqueChars = 1;

        idOptions.SignIn.RequireConfirmedEmail = false;

        // Lockout settings.
        idOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        idOptions.Lockout.MaxFailedAccessAttempts = 5;
        idOptions.Lockout.AllowedForNewUsers = true;
        
        options.Setup(o => o.Value).Returns(idOptions);
        var userValidators = new List<IUserValidator<AppUser>>();
        var validator = new UserValidator<AppUser>();

        var passValidator = new PasswordValidator<AppUser>();
        var pwdValidators = new List<IPasswordValidator<AppUser>>();
        pwdValidators.Add(passValidator);
        
        var userManager = new UserManager<AppUser>(claimStore.Object,
            options.Object,
            new PasswordHasher<AppUser>(),
            userValidators,
            pwdValidators,
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(), 
            null,
            new Mock<ILogger<UserManager<AppUser>>>().Object);
        
        return userManager;
    }
}