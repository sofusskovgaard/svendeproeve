using App.Infrastructure.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace App.Infrastructure.Extensions;

public static class RabbitMqRegistrator
{
    /// <summary>
    /// Add RabbitMQ and it's command handlers to service collection. Uses the <see cref="TTypeAssemblyToSearch" /> generic type to choose a namespace to search in. 
    /// </summary>
    /// <param name="services">The target <see cref="IServiceCollection"/></param>
    /// <param name="config">An optional configuration</param>
    /// <typeparam name="TTypeAssemblyToSearchT">The type assembly to search</typeparam>
    public static void AddRabbitMq<TTypeAssemblyToSearch>(this IServiceCollection services,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? config = null)
    {
        RabbitMqRegistrator.AddRabbitMq(services, typeof(TTypeAssemblyToSearch), config);
    }

    /// <summary>
    /// Add RabbitMQ and it's command handlers to service collection. Uses the <see cref="Type" /> to choose a namespace to search in.
    /// </summary>
    /// <param name="services">The target <see cref="IServiceCollection"/></param>
    /// <param name="typeAssemblyToSearch">The type assembly to search</param>
    /// <param name="config">An optional configuration</param>
    public static void AddRabbitMq(this IServiceCollection services, Type typeAssemblyToSearch,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? config = null)
    {
        services.AddMassTransit(options =>
        {
            options.SetKebabCaseEndpointNameFormatter();

            options.AddConsumersFromNamespaceContaining(typeAssemblyToSearch);

            options.UsingRabbitMq((context, configure) =>
            {
                var options = context.GetService<IOptions<RabbitMQOptions>>();

                configure.Host(options.Value.Host, configurator =>
                {
                    configurator.Username(options.Value.Username);
                    configurator.Password(options.Value.Password);
                });

                configure.AutoDelete = true;

                config?.Invoke(context, configure);

                configure.ConfigureEndpoints(context);
            });
        });
    }
}