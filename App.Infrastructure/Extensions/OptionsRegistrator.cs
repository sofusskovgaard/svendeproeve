using App.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Extensions;

public static class OptionsRegistrator
{
    public static void RegisterOptions(this IServiceCollection services)
    {
        services.AddOptions<RabbitMQOptions>();
        services.AddOptions<DatabaseOptions>();
    }
}