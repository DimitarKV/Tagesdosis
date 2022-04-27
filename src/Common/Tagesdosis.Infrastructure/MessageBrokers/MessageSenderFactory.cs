using Tagesdosis.Application.Infrastructure.MessageBrokers;
using Tagesdosis.Domain.Events;
using Tagesdosis.Infrastructure.MessageBrokers.AzureServiceBus;

namespace Tagesdosis.Infrastructure.MessageBrokers;

public class MessageSenderFactory : IMessageSenderFactory
{
    private readonly string _connectionString;

    public MessageSenderFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IMessageSender<T> CreateAzureTopicSender<T>(string topic) where T : IDomainEvent
    {
        return new AzureServiceBusTopicSender<T>(_connectionString, topic);
    }
    
    public IMessageSender<T> CreateAzureQueueSender<T>(string queue) where T : IDomainEvent
    {
        return new AzureServiceBusSender<T>(_connectionString, queue);
    }
}