using Tagesdosis.Domain.Events;

namespace Tagesdosis.Application.Infrastructure.MessageBrokers;

public interface IMessageSender<in T> where T : IDomainEvent
{
    public Task SendAsync(T message, MessageMetaData metaData, CancellationToken cancellationToken);
}