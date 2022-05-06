namespace Tagesdosis.Application.Infrastructure.ServiceConsumers;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class BaseUrlAttribute : Attribute
{
    public string Url { get; private set; }
    public bool LoadFromConfiguration { get; set; }
    
    public BaseUrlAttribute(string url, bool loadFromConfiguration = false)
    {
        Url = url;
        LoadFromConfiguration = loadFromConfiguration;
    }

}