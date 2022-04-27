namespace Tagesdosis.Domain.Events;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class DomainEventHandlerConfigurationAttribute : Attribute
{
    public string Topic { get; set; }
    public string Subscription { get; set; }
    
    public DomainEventHandlerConfigurationAttribute(string topic, string subscription)
    {
        Topic = topic;
        Subscription = subscription;
    }
}