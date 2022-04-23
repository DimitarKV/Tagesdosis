using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tagesdosis.Tests.Common;

public class InMemoryServiceCollection<T> where T : DbContext
{
    private readonly ServiceCollection _serviceCollection;
    private readonly ServiceProvider _serviceProvider;

    public InMemoryServiceCollection(Action<IServiceCollection> action)
    {
        _serviceCollection = new ServiceCollection();

        _serviceCollection.AddDbContext<T>(options => options.UseInMemoryDatabase("tests"));
        _serviceCollection.AddLogging();
        action.Invoke(_serviceCollection);

        DbContext = _serviceCollection
            .BuildServiceProvider()
            .GetService<T>()!;
        DbContext.Database.EnsureCreated();

        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    public T DbContext { get; set; }

    public U? GetService<U>()
    {
        return _serviceProvider.GetService<U>();
    }
}