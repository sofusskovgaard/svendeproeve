using System.Reflection;
using App.Infrastructure.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace App.Infrastructure.Extensions;

public static class RabbitMqRegistrator
{
    /// <summary>
    ///     Add RabbitMQ and it's command handlers to service collection. Uses the <see cref="TTypeAssemblyToSearch" /> generic
    ///     type to choose a namespace to search in.
    /// </summary>
    /// <param name="services">The target <see cref="IServiceCollection" /></param>
    /// <param name="config">An optional configuration</param>
    /// <typeparam name="TTypeAssemblyToSearchT">The type assembly to search</typeparam>
    //public static void AddRabbitMq(this IServiceCollection services, Assembly assemblyToScan,
    //    Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? config = null)
    //{
    //    var typeInAssembly = assemblyToScan.GetTypes().FirstOrDefault(type => type.GetInterfaces().Any(iface =>
    //        iface switch
    //        {
    //            ICommandHandler => true,
    //            IEventHandler => true,
    //            _ => false
    //        }));

    //    if ()
    //    AddRabbitMq(services, typeInAssembly, config);
    //}

    /// <summary>
    /// Add RabbitMQ and it's command handlers to service collection. Uses the <see cref="Assembly" /> to choose a namespace to search in.
    /// </summary>
    /// <param name="services">The target <see cref="IServiceCollection"/></param>
    /// <param name="assemblyToScan">The assembly to search</param>
    /// <param name="config">An optional configuration</param>
    public static void AddRabbitMq(this IServiceCollection services, Assembly assemblyToScan,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? config = null)
    {
        services.AddMassTransit(options =>
        {
            options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));

            options.AddConsumers(assemblyToScan);

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
    
    public static void AddRabbitMq(this IServiceCollection services, Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? config = null)
    {
        services.AddMassTransit(options =>
        {
            options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));

            options.UsingRabbitMq((context, configure) =>
            {
                var settings = context.GetService<IOptions<RabbitMQOptions>>();

                configure.Host(settings.Value.Host, configurator =>
                {
                    configurator.Username(settings.Value.Username);
                    configurator.Password(settings.Value.Password);
                });

                config?.Invoke(context, configure);
            });
        });
    }
}