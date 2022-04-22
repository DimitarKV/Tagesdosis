using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;

    public IdentityService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public Task<AppUser> FindByNameAsync(string? name)
    {
        return _userManager.FindByNameAsync(name);
    }

    public Task<IdentityResult> AddClaimAsync(AppUser user, Claim claim)
    {
        return _userManager.AddClaimAsync(user, claim);
    }

    public Task<IList<Claim>> GetClaimsAsync(AppUser user)
    {
        return _userManager.GetClaimsAsync(user);
    }

    public Task<IdentityResult> CreateAsync(AppUser user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }

    public Task<IdentityResult> DeleteAsync(AppUser user)
    {
        return _userManager.DeleteAsync(user);
    }

    public Task<IdentityResult> ChangePasswordAsync(AppUser user, string oldPassword, string newPassword)
    {
        return _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
    }

    public Task<IdentityResult> UpdateAsync(AppUser user)
    {
        return _userManager.UpdateAsync(user);
    }

    public Task<bool> CheckPasswordAsync(AppUser user, string password)
    {
        return _userManager.CheckPasswordAsync(user, password);
    }
}