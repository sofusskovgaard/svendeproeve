using App.Services.Gateway.Infrastructure.Options;
using App.Services.Gateway.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Gateway.Infrastructure.Extensions;

public static class RedisRegistrator
{
    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        services.AddOptions<RedisOptions>();
        services.AddScoped<IRedisCache, RedisCache>();

        return services;
    }
}