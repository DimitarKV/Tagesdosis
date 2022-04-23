using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Data.Persistence;
using Tagesdosis.Services.User.Extensions;
using Tagesdosis.Services.User.Identity;
using Tagesdosis.Services.User.MappingProfiles;
using Tagesdosis.Tests.Common;

namespace Tagesdosis.Tests.User;

public class UserTestsBase
{
    protected readonly IMapper Mapper;
    protected readonly IIdentityService IdentityService;
    protected InMemoryServiceCollection<UserDbContext> InMemory;

    public UserTestsBase()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserProfile());
            cfg.AddProfile(new TokenProfile());
        });
        Mapper = new Mapper(mapperConfiguration);
        
        InMemory = new InMemoryServiceCollection<UserDbContext>(services => services.AddIdentity());
        IdentityService = new IdentityService(InMemory.GetService<UserManager<AppUser>>()!);
    }

    protected async Task CreateUserAsync(string userName, string password)
    {
        var user = new AppUser(userName);
        await IdentityService.CreateAsync(user, password);
    }
    
    protected async Task DeleteUserAsync(string userName)
    {
        var user = new AppUser(userName);
        await IdentityService.DeleteAsync(user);
    }
}