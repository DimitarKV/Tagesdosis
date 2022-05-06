namespace Tagesdosis.Application.Infrastructure.ServiceConsumers;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class ContentTypeAttribute : Attribute
{
    public string Content { get; private set; }

    public ContentTypeAttribute(string content)
    {
        Content = content;
    }

}