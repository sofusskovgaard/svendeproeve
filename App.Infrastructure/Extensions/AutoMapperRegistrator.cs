using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Extensions;

public static class AutoMapperRegistrator
{
    /// <summary>
    /// Add AutoMapper to the dependency container with automatic registration of mapper definitions using an assembly.
    /// </summary>
    /// <param name="serviceCollection">The target service collection</param>
    /// <param name="assemblyToScan">Assembly to scan for mapper definitions</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection, Assembly assemblyToScan) =>
        serviceCollection.AddAutoMapper(cfg => cfg.AddMaps(assemblyToScan));
}