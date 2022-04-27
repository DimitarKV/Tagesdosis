using Tagesdosis.Domain.Events;

namespace Tagesdosis.Application.Infrastructure.MessageBrokers;

public interface IMessageSenderFactory
{
    public IMessageSender<T> CreateAzureTopicSender<T>(string topic) where T : IDomainEvent;
    public IMessageSender<T> CreateAzureQueueSender<T>(string queue) where T : IDomainEvent;
}