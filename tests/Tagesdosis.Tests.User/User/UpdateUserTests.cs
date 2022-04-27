using System.Threading;
using System.Threading.Tasks;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Commands.User.UpdateUserCommand;
using Tagesdosis.Tests.Common;
using Xunit;

namespace Tagesdosis.Tests.User.User;

public class UpdateUserTests : UserTestsBase
{
    [Theory]
    [InlineData("johndoe", "#Password123", "johndoe1")]
    public async Task UpdateUser_ShouldReturnOk(string userName, string password, string newUserName)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();
        await CreateUserAsync(userName, password);
        
        var command = new UpdateUserCommand
        {
            UserName = userName,
            CurrentPassword = password,
            NewUserName = newUserName
        };
        var handler = new UpdateUserCommandHandler(Mapper, IdentityService);
        var validator = new UpdateUserCommandValidator(IdentityService);

        Assert.True((await validator.ValidateAsync(command)).IsValid);
        
        var response = await handler.Handle(command, CancellationToken.None);
        Assert.True(response!.IsValid);
    }
    
    [Theory]
    [InlineData("johndoe", "#Password123", "johndoe1")]
    public async Task UpdateUser_ShouldReturnInvalid(string userName, string password, string newUserName)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();
        
        var command = new UpdateUserCommand
        {
            UserName = userName,
            CurrentPassword = password,
            NewUserName = newUserName
        };
        var handler = new UpdateUserCommandHandler(Mapper, IdentityService);
        var validator = new UpdateUserCommandValidator(IdentityService);

        Assert.True((await validator.ValidateAsync(command)).IsValid);
        
        var response = await handler.Handle(command, CancellationToken.None);
        Assert.False(response.IsValid);
    }
}