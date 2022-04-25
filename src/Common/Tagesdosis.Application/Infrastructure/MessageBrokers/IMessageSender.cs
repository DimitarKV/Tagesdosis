namespace Tagesdosis.Application.Infrastructure.MessageBrokers;

public interface IMessageSender<in T>
{
    public Task SendAsync(T message, MessageMetaData metaData, CancellationToken cancellationToken);
}