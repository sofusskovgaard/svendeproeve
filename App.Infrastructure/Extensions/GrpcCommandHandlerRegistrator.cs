using System.Reflection;
using App.Common.Grpc;
using App.Infrastructure.Grpc;
using App.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Extensions;

public static class GrpcCommandHandlerRegistrator
{
    public static void AddGrpcCommandHandlers(this IServiceCollection services, Assembly? commandHandlerAssembly)
    {
        foreach (var handlerType in DiscoveryHelper.Discover<IGrpcCommandHandler>(commandHandlerAssembly)!)
        {
            var handlerInterface =
                handlerType.GetInterfaces().First(x => x.GetInterface(nameof(IGrpcCommandHandler)) != null);

            if (handlerInterface.GenericTypeArguments[0].GetInterface(nameof(IGrpcCommandMessage)) != null &&
                handlerInterface.GenericTypeArguments[1].GetInterface(nameof(IGrpcCommandResult)) != null)
                services.AddTransient(handlerType);
        }
    }
}