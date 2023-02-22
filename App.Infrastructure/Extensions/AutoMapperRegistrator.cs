using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Extensions;

public static class AutoMapperRegistrator
{
    /// <summary>
    /// Add AutoMapper to the dependency container with automatic registration of mapper definitions using a type.
    /// </summary>
    /// <param name="serviceCollection">The target service collection</param>
    /// <param name="typeInAssemblyToScan">A type within the assembly to scan for mapper definitions</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection, Type typeInAssemblyToScan) =>
        serviceCollection.AddAutoMapper(expression => expression.AddMaps(typeInAssemblyToScan));
}