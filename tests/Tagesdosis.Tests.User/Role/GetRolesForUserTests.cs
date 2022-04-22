using System.Threading;
using System.Threading.Tasks;
using Tagesdosis.Services.User.Queries.Role.GetRolesForUser;
using Tagesdosis.Services.User.Queries.User.GetUserQuery;
using Xunit;

namespace Tagesdosis.Tests.User.Role;

public class GetRolesForUserTests  : UserTestsBase
{
    [Theory]
    [InlineData("johndoe", "#Password123")]
    public async Task GetRolesForUser_ShouldReturnOk(string userName, string password)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();

        await CreateUserAsync(userName, password);
        
        var query = new GetRolesForUserQuery(userName);
        var handler = new GetRolesForUserQueryHandler(IdentityService);
        var validator = new GetRolesForUserQueryValidator(IdentityService);

        Assert.True((await validator.ValidateAsync(query)).IsValid);
        
        var response = await handler.Handle(query, CancellationToken.None);
        
        Assert.True(response.IsValid);

        await DeleteUserAsync(userName);
    }
    
    [Theory]
    [InlineData("johndoe")]
    public async Task GetUser_ShouldReturnInvalid(string userName)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();

        var query = new GetRolesForUserQuery(userName);
        var handler = new GetRolesForUserQueryHandler(IdentityService);
        var validator = new GetRolesForUserQueryValidator(IdentityService);

        Assert.False((await validator.ValidateAsync(query)).IsValid);
    }
}