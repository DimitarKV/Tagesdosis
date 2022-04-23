using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Tagesdosis.Services.User.Commands.Token.CreateTokenCommand;
using Xunit;

namespace Tagesdosis.Tests.User.Token;

public class CreateTokenTests : UserTestsBase
{
    [Theory]
    [InlineData("johndoe", "#Password123")]
    public async Task CreateToken_ShouldReturnOk(string userName, string password)
    {
        await InMemory.DbContext.Database.EnsureDeletedAsync();
        await InMemory.DbContext.Database.EnsureCreatedAsync();

        await CreateUserAsync(userName, password);
        
        var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Issuer", "tagesdosis"},
            {"Jwt:Audience", "tagesdosis"},
            {"Jwt:Key", "SuperSecretKey12345"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        var command = new CreateTokenCommand(userName, password);
        var handler = new CreateTokenCommandHandler(configuration, IdentityService);
        var validator = new CreateTokenCommandValidator(IdentityService);

        Assert.True((await validator.ValidateAsync(command)).IsValid);
        
        var response = await handler.Handle(command, CancellationToken.None);
        
        Assert.True(response.IsValid);
    }
}