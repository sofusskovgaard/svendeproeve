using App.Infrastructure.Events;
using MassTransit;
using App.Services.Users.Infrastructure.Events;
using Microsoft.Extensions.Logging;

namespace App.Services.Users.Infrastructure.EventHandlers;

public class TestEventHandler : IEventHandler<TestEventMessage>
{
    private readonly ILogger<TestEventHandler> _logger;

    public TestEventHandler(ILogger<TestEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TestEventMessage> context)
    {
        _logger.LogInformation("Consumed a event message");
        return Task.CompletedTask;
    }
}