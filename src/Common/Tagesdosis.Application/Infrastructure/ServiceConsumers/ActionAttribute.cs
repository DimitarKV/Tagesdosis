namespace Tagesdosis.Application.Infrastructure.ServiceConsumers;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class ActionAttribute : Attribute
{
    public string Endpoint { get; private set; }
    public string Method { get; private set; }

    public ActionAttribute(string endpoint, string method)
    {
        Endpoint = endpoint;
        Method = method;
    }

}