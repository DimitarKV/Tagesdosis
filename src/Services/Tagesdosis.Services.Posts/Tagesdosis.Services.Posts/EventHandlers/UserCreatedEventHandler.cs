using Tagesdosis.Domain.Events;

namespace Tagesdosis.Services.Posts.EventHandlers;

public class UserCreatedEvent : IDomainEvent
{
    
}

[DomainEventHandlerProperties("user", "Posts-Srv")]
public class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    public async Task HandleAsync(UserCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        
    }
}