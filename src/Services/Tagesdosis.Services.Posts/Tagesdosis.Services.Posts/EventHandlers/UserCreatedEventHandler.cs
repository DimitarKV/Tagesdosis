using Microsoft.Extensions.DependencyInjection;
using Tagesdosis.Domain.Events;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.EventHandlers;

public class UserCreatedEvent : DomainEvent
{
    public string Id { get; set; }
    public string UserName { get; set; }
}


[DomainEventHandlerConfiguration("user", "Post-Srv")]
public class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    private readonly IServiceProvider _provider;

    public UserCreatedEventHandler(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    public async Task HandleAsync(UserCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // A scope MUST be created, because
        // EventHandlers are registered as Singletons, but DbContexts and Repositories are scoped(Scoped or Transient)
        // Singletons > Scoped
        using var scope = _provider.CreateScope();
        var repository = scope.ServiceProvider.GetService<IAuthorRepository>();
        
        // TODO: Inject mediator and use commands, the following is just an example
        var author = new Author
        {
            UserName = domainEvent.UserName,
            ExternalId = domainEvent.Id,
            CreatedOn = DateTime.Now,
            UpdatedOn = DateTime.Now
        };
        await repository.SaveAuthorAsync(author);
    }
}