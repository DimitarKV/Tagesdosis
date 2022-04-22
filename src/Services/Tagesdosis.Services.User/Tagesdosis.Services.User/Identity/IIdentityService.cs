using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;

namespace Tagesdosis.Services.User.Identity;

public interface IIdentityService
{
    public Task<AppUser?> FindByNameAsync(string? userName);
    public Task<IdentityResult> AddClaimAsync(AppUser user, Claim claim);
    public Task<IList<Claim>> GetClaimsAsync(AppUser user);
    public Task<IdentityResult> CreateAsync(AppUser user, string password);
    public Task<IdentityResult> DeleteAsync(AppUser user);
    public Task<IdentityResult> ChangePasswordAsync(AppUser user, string oldPassword, string newPassword);
    public Task<IdentityResult> UpdateAsync(AppUser user);
    public Task<bool> CheckPasswordAsync(AppUser user, string password);
}