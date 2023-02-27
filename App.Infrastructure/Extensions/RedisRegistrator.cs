using App.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Extensions;

public static class RedisRegistrator
{
    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        services.AddScoped<IRedisCache, RedisCache>();

        return services;
    }
}