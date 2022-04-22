using System.Threading;
using System.Threading.Tasks;
using Tagesdosis.Services.User.Commands.Role.AddRoleToUserCommand;
using Xunit;

namespace Tagesdosis.Tests.User.Role;

public class AddRoleToUserTests : UserTestsBase
{
    [Theory]
    [InlineData("johndoe", "#Password123", "Admin")]
    public async Task AddRoleToUser_ShouldReturnOk(string userName, string password, string role)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();

        await CreateUserAsync(userName, password);
        
        var command = new AddRoleToUserCommand(userName, role);
        var handler = new AddRoleToUserCommandHandler(IdentityService);
        var validator = new AddRoleToUserCommandValidator(IdentityService);

        Assert.True((await validator.ValidateAsync(command)).IsValid);
        
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.True(response.IsValid);
    }
}