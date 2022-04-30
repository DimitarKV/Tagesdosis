using Tagesdosis.Domain.Events;

namespace Tagesdosis.Services.User.Commands.User.DeleteUserCommand;

public class UserDeletedEvent : DomainEvent
{
    public string Id { get; set; }

    public UserDeletedEvent(string id)
    {
        Id = id;
    }
}