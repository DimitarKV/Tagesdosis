namespace Tagesdosis.Application.Infrastructure.ProtectedStorage;

public interface IProtectedStorage
{
    public string Read(string key);
}