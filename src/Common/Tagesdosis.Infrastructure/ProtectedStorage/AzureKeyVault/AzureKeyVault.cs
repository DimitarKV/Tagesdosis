using Microsoft.Extensions.Configuration;
using Tagesdosis.Application.Infrastructure.ProtectedStorage;

namespace Tagesdosis.Infrastructure.ProtectedStorage.AzureKeyVault;

public class AzureKeyVault : IProtectedStorage
{
    private readonly IConfiguration _configuration;

    public AzureKeyVault(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Read(string key)
    {
        return _configuration[key];
    }
}