using System.Threading;
using System.Threading.Tasks;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Commands.User.DeleteUserCommand;
using Tagesdosis.Tests.Common;
using Xunit;

namespace Tagesdosis.Tests.User.User;

public class DeleteUserTests : UserTestsBase
{
    [Theory]
    [InlineData("johndoe", "#Password123")]
    public async Task DeleteUser_ShouldReturnOk(string userName, string password)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();
        await CreateUserAsync(userName, password);
        
        var command = new DeleteUserCommand(userName);
        var handler = new DeleteUserCommandHandler(IdentityService);
        var validator = new DeleteUserCommandValidator(IdentityService);

        Assert.True((await validator.ValidateAsync(command)).IsValid);
        
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.True(response!.IsValid);
    }
    
    [Theory]
    [InlineData("johndoe", "#Password123")]
    public async Task DeleteUser_ShouldReturnInvalid(string userName, string password)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();
        
        var command = new DeleteUserCommand(userName);
        var handler = new DeleteUserCommandHandler(IdentityService);
        var validator = new DeleteUserCommandValidator(IdentityService);

        bool valid = (await validator.ValidateAsync(command)).IsValid;
        Assert.False(valid);
    }
}