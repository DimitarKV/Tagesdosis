using Tagesdosis.Domain.Events;

namespace Tagesdosis.Application.Infrastructure.MessageBrokers;

public class Message<T> where T : IDomainEvent
{
    public T Data { get; set; }
    public MessageMetaData MetaData { get; set; }
}