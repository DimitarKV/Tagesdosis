using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tagesdosis.Application.Infrastructure.MessageBrokers;
using Tagesdosis.Domain.Events;
using Tagesdosis.Domain.Extensions;
using Tagesdosis.Infrastructure.MessageBrokers.AzureServiceBus;

namespace Tagesdosis.Infrastructure.MessageBrokers.Extensions;

public static class AzureServiceBusExtensions
{
    private static IServiceProvider Provider { get; set; }
    
    public static void AddAzureServiceBusReceivers(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        var method = typeof(AzureServiceBusExtensions).GetMethod(nameof(CreateInstance), BindingFlags.Static | BindingFlags.NonPublic);

        foreach (var type in DomainEventExtensions.Handlers)
        {
            var genericInterface = type
                .GetInterfaces()
                .SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

            if (genericInterface is not null)
            {
                var attribute = type.GetCustomAttribute<DomainEventHandlerConfigurationAttribute>();
                if (attribute is null) break;
                
                var eventType = genericInterface.GetGenericArguments().First();

                var genericMethod = method!.MakeGenericMethod(eventType);
                var receiver = genericMethod.Invoke(null, new object?[]
                {
                    configuration["AzureServiceBus:ConnectionString"],
                    attribute.Topic,
                    attribute.Subscription
                });

                services.AddSingleton(receiver!);
            }
        }
    }

    public static void UseAzureServiceBusReceivers(this WebApplication app)
    {
        Provider = app.Services;
    }

    private static IMessageReceiver CreateInstance<T>(
        string connectionString, string topicName, string subscriptionName)
    where T : IDomainEvent
    {
        var receiver = new AzureServiceBusSubscriptionReceiver<T>(connectionString, topicName, subscriptionName);
        receiver.Receive((data, metaData) =>
        {
            var handler = Provider.GetService<IDomainEventHandler<T>>();
            handler?.HandleAsync(data);
        });

        return receiver;
    }
}