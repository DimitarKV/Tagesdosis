namespace Tagesdosis.Domain.Events;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class DomainEventHandlerPropertiesAttribute : Attribute
{
    public string Topic { get; set; }
    public string Subscription { get; set; }
    
    public DomainEventHandlerPropertiesAttribute(string topic, string subscription)
    {
        Topic = topic;
        Subscription = subscription;
    }
}