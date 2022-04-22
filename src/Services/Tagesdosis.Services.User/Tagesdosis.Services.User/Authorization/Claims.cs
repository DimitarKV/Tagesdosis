using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Tagesdosis.Services.User.Authorization;

public static class Claims
{
    public const string UserRole = "User";
    public const string AdminRole = "Admin";

    public static Claim User = new Claim(ClaimTypes.Role, UserRole);
    public static Claim Admin = new Claim(ClaimTypes.Role, AdminRole);
}