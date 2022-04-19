namespace Tagesdosis.Domain.Entities;

public abstract class Entity<T>
{
    public T Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}