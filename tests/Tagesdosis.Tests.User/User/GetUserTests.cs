using System.Threading;
using System.Threading.Tasks;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Queries.User.GetUserQuery;
using Xunit;

namespace Tagesdosis.Tests.User.User;

public class GetUserTests : UserTestsBase
{
    [Theory]
    [InlineData("johndoe", "#Password123")]
    public async Task GetUser_ShouldReturnOk(string userName, string password)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();

        await CreateUserAsync(userName, password);
        
        var query = new GetUserQuery(userName);
        var handler = new GetUserQueryHandler(IdentityService, Mapper);
        var validator = new GetUserQueryValidator(IdentityService);

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

        var query = new GetUserQuery(userName);
        var handler = new GetUserQueryHandler(IdentityService, Mapper);
        var validator = new GetUserQueryValidator(IdentityService);

        Assert.False((await validator.ValidateAsync(query)).IsValid);
    }
}