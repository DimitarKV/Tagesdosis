using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Tagesdosis.Application.Infrastructure.MessageBrokers;
using Tagesdosis.Domain.Events;

namespace Tagesdosis.Infrastructure.MessageBrokers.AzureServiceBus;

public class AzureServiceBusTopicSender<T> : IMessageSender<T>
    where T : IDomainEvent
{
    private readonly ServiceBusSender _sender;
    private readonly string _topicName;
    
    public AzureServiceBusTopicSender(ServiceBusSender sender, string topicName)
    {
        _sender = sender;
        _topicName = topicName;
    }
    
    public async Task SendAsync(T message, MessageMetaData metaData, CancellationToken cancellationToken)
    {
        message.Type = typeof(T).Name;
        var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Message<T>
        {
            Data = message,
            MetaData = metaData
        })));

        await _sender.SendMessageAsync(serviceBusMessage, cancellationToken);
    }
}