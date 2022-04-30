using Tagesdosis.Domain.Events;

namespace Tagesdosis.Services.User.Commands.User.UpdateUserCommand;

/// <summary>
/// Represents a changed username
/// </summary>
public class UserUpdatedEvent : DomainEvent
{
    public string Id { get; set; }
    public string UserName { get; set; }
}