using App.Infrastructure.Events;
using App.Services.Users.Infrastructure.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace App.Services.Users.Infrastructure.EventHandlers;

public class SecondTestEventHandler : IEventHandler<UserCreatedEventMessage>
{
    private readonly ILogger<SecondTestEventHandler> _logger;

    public SecondTestEventHandler(ILogger<SecondTestEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserCreatedEventMessage> context)
    {
        _logger.LogInformation("Consumed a event message");
        return Task.CompletedTask;
    }
}