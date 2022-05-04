using System.Reflection;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tagesdosis.Domain.Events;

namespace Tagesdosis.Infrastructure.MessageBrokers.Extensions;

public delegate ServiceBusSender AzureServiceBusSenderResolver(string key);

public static class AzureServiceBusSenderExtensions
{
    public static void AddAzureServiceBusSenders(this WebApplicationBuilder builder, Assembly assembly)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        
        services.AddSingleton(
            _ => new ServiceBusClient(configuration["AzureServiceBus:ConnectionString"]));

        var topics = configuration
            .GetSection("AzureServiceBus:Topics")
            .Get<string[]>();

        var dictionary = new Dictionary<string, ServiceBusSender>();
        
        foreach (var topic in topics)
        {
            services.AddSingleton(
                provider => provider.GetService<ServiceBusClient>()!.CreateSender(topic));
        }

        services.AddSingleton<AzureServiceBusSenderResolver>(provider => key =>
        {
            var senders = provider.GetServices<ServiceBusSender>();
            return senders.FirstOrDefault(sender => sender.EntityPath == key);
        });
    }
}