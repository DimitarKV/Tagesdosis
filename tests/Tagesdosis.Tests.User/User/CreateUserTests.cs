using System.Threading;
using System.Threading.Tasks;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Commands.User.DeleteUserCommand;
using Xunit;

namespace Tagesdosis.Tests.User.User;

public class CreateUserTests : UserTestsBase
{
    [Theory]
    [InlineData("johndoe", "johndoe@mail.com", "#Password123")]
    public async Task CreateUser_ShouldReturnOk(string userName, string email, string password)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();
        
        var command = new CreateUserCommand(userName, email, password);
        var handler = new CreateUserCommandHandler(Mapper, IdentityService);
        var validator = new CreateUserCommandValidator(IdentityService);

        Assert.True((await validator.ValidateAsync(command)).IsValid);
        
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.True(response.IsValid);

        await DeleteUserAsync(userName);
    }
    
    [Theory]
    [InlineData("johndoe", "johndoe@mail.com", "invalidpassword")]
    public async Task CreateUser_ShouldReturnInvalid(string userName, string email, string password)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();
        
        var command = new CreateUserCommand(userName, email, password);
        var handler = new CreateUserCommandHandler(Mapper, IdentityService);
        var validator = new CreateUserCommandValidator(IdentityService);

        Assert.True((await validator.ValidateAsync(command)).IsValid);
        
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.False(response.IsValid);
    }
}