using Tagesdosis.Domain.Events;

namespace Tagesdosis.Services.Posts.EventHandlers;

public class UserCreatedEvent : DomainEvent
{
    public string Message { get; set; }
}

public class UserUpdatedEvent : DomainEvent
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


[DomainEventHandlerConfiguration("user", "Post-Srv")]
public class UserUpdatedEventHandler : IDomainEventHandler<UserUpdatedEvent>
{
    public async Task HandleAsync(UserUpdatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(domainEvent.Message);
    }
}