using Tagesdosis.Domain.Events;

namespace Tagesdosis.Services.User.Commands.User.CreateUserCommand;

public class UserCreatedEvent : DomainEvent
{
    public string Id { get; set; }
    public string UserName { get; set; }

    public UserCreatedEvent(string id, string userName)
    {
        Id = id;
        UserName = userName;
    }
}