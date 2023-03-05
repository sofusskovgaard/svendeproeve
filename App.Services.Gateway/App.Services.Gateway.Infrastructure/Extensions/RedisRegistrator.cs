using App.Services.Gateway.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Gateway.Infrastructure.Extensions;

public static class RedisRegistrator
{
    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        services.AddScoped<IRedisCache, RedisCache>();

        return services;
    }
}