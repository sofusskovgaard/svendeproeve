using App.Infrastructure.Commands;
using MassTransit;
using App.Services.Users.Infrastructure.Commands;
using App.Services.Users.Infrastructure.Events;
using Microsoft.Extensions.Logging;

namespace App.Services.Users.Infrastructure.CommandHandlers;

public class TestCommandHandler : ICommandHandler<TestCommandMessage>
{
    private readonly ILogger<TestCommandHandler> _logger;

    public TestCommandHandler(ILogger<TestCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TestCommandMessage> context)
    {
        _logger.LogInformation("Consumed a command message");
        return context.Publish(new TestEventMessage());
    }
}