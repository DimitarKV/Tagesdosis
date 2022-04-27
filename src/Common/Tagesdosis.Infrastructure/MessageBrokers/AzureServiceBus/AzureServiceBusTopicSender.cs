using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Tagesdosis.Application.Infrastructure.MessageBrokers;
using Tagesdosis.Domain.Events;

namespace Tagesdosis.Infrastructure.MessageBrokers.AzureServiceBus;

public class AzureServiceBusTopicSender<T> : IMessageSender<T>
    where T : IDomainEvent
{
    private readonly string _connectionString;
    private readonly string _topicName;
    
    public AzureServiceBusTopicSender(string connectionString, string topicName)
    {
        _connectionString = connectionString;
        _topicName = topicName;
        
    }
    
    public async Task SendAsync(T message, MessageMetaData metaData, CancellationToken cancellationToken)
    {
        var client = new ServiceBusClient(_connectionString);
        var sender = client.CreateSender(_topicName);
        
        message.Type = typeof(T).Name;
        var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Message<T>
        {
            Data = message,
            MetaData = metaData
        })));

        await sender.SendMessageAsync(serviceBusMessage, cancellationToken);
    }
}