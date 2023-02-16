using App.Infrastructure.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace App.Infrastructure.Extensions;

public static class RabbitMqRegistrator
{
    public static void AddRabbitMq(this IServiceCollection services, Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? config = null)
    {
        services.AddMassTransit(options =>
        {
            options.SetKebabCaseEndpointNameFormatter();

            options.UsingRabbitMq((context, configure) =>
            {
                var options = context.GetService<IOptions<RabbitMQOptions>>();

                configure.Host(options.Value.Host, configurator =>
                {
                    configurator.Username(options.Value.Username);
                    configurator.Password(options.Value.Password);
                });

                config?.Invoke(context, configure);
            });
        });
    }

    public static void AddRabbitMq<T>(this IServiceCollection services, Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? config = null)
    {
        services.AddMassTransit(options =>
        {
            options.SetKebabCaseEndpointNameFormatter();

            options.AddConsumersFromNamespaceContaining<T>();

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