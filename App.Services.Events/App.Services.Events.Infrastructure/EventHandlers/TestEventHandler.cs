using App.Infrastructure.Events;
using App.Services.Events.Infrastructure.Events;
using MassTransit;

namespace App.Services.Events.Infrastructure.EventHandlers;

public class TestEventHandler : IEventHandler<TestEventMessage>
{
    public Task Consume(ConsumeContext<TestEventMessage> context)
    {
        throw new NotImplementedException();
    }
}