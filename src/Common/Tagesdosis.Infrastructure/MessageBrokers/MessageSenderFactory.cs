using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Tagesdosis.Application.Infrastructure.MessageBrokers;
using Tagesdosis.Domain.Events;
using Tagesdosis.Infrastructure.MessageBrokers.AzureServiceBus;
using Tagesdosis.Infrastructure.MessageBrokers.Extensions;

namespace Tagesdosis.Infrastructure.MessageBrokers;

public class MessageSenderFactory : IMessageSenderFactory
{
    private readonly AzureServiceBusSenderResolver _resolver;

    public MessageSenderFactory(AzureServiceBusSenderResolver resolver)
    {
        _resolver = resolver;
    }
    
    public IMessageSender<T> CreateAzureTopicSender<T>(string topic) where T : IDomainEvent
    {
        var sender = _resolver(topic);
        return new AzureServiceBusTopicSender<T>(sender, topic);
    }
    
    public IMessageSender<T> CreateAzureQueueSender<T>(string queue) where T : IDomainEvent
    {
        var sender = _resolver(queue);
        return new AzureServiceBusTopicSender<T>(sender, queue);
    }
}