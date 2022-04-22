using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Data.Persistence;

namespace Tagesdosis.Services.User.Extensions;

public static class IdentityExtensions
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
    }
}