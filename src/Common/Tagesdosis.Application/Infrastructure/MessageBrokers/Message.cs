namespace Tagesdosis.Application.Infrastructure.MessageBrokers;

public class Message<T>
{
    public T Data { get; set; }
    public MessageMetaData MetaData { get; set; }
}