using Tagesdosis.Domain.Events;

namespace Tagesdosis.Services.Posts.EventHandlers;

public class UserCreatedEvent : IDomainEvent
{
    public string Message { get; set; }
}

[DomainEventHandlerConfiguration("user", "Post-Srv")]
public class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    public async Task HandleAsync(UserCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(domainEvent.Message);
    }
}