namespace Tagesdosis.Domain.Entities;

public interface IEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}