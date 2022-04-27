namespace Tagesdosis.Domain.Events;

public interface IDomainEvent
{
    public string Type { get; set; }
}

public class DomainEvent : IDomainEvent
{
    public string Type { get; set; }
}