using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;

namespace App.Infrastructure.Extensions;

public static class GrpcServiceRegistrator
{
    public static void AddGrpcServiceClient<TGrpcService>(this IServiceCollection services) where TGrpcService : class
    {
        var attr =
            (ServiceAttribute)typeof(TGrpcService).GetCustomAttribute(
                typeof(ServiceAttribute))!;

        services.AddCodeFirstGrpcClient<TGrpcService>(options =>
        {
            options.Address = new Uri($"http://{attr.Name}");
        });
    }
}