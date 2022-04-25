using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tagesdosis.Domain.Events;

namespace Tagesdosis.Domain.Extensions;

public static class DomainEventExtensions
{
    public static List<Type> Handlers { get; private set; }

    public static void AddDomainEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        Handlers = assembly
            .GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)))
            .ToList();

        foreach (var handler in Handlers)
        {
            var interfaces = handler.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>))
                .ToList();
            var type = interfaces[0];
            
            services.AddTransient(type, handler);
        }
    }
}