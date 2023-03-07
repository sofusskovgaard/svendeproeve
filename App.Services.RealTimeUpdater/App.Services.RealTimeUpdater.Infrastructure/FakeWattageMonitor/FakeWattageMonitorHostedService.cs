using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.RealTimeUpdater.Infrastructure.FakeWattageMonitor;

public class FakeWattageMonitorHostedService : IHostedService, IDisposable
{
    private readonly ILogger<FakeWattageMonitorHostedService> _logger;

    private readonly IServiceScopeFactory _scopeFactory;

    private string[] locations = { "here", "there" };

    private Timer? _timer;

    public FakeWattageMonitorHostedService(ILogger<FakeWattageMonitorHostedService> logger, IServiceScopeFactory scopeFactory)
    {
        this._logger = logger;
        this._scopeFactory = scopeFactory;
    }

    private void Callback(object? state)
    {
        var scope = _scopeFactory.CreateScope();
        var bus = scope.ServiceProvider.GetRequiredService<IBus>();

        bus.Publish(new WattageUpdatedEvent
        {
            Location = locations[RandomNumberGenerator.GetInt32(this.locations.Length)],
            Wattage = RandomNumberGenerator.GetInt32(10, 16)
        });
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Starting fake wattage monitor");

        this._timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Stopping fake wattage monitor");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        this._timer?.Dispose();
    }
}
