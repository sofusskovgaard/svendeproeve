using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace App.Infrastructure.Extensions;

public static class SerilogRegistrator
{
    public static void RegisterSerilog(this IHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Quartz", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        host.UseSerilog();
    }
}