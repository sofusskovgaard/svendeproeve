using App.Infrastructure.Events;
using MassTransit;
using App.Services.Users.Infrastructure.Events;

namespace App.Services.Users.Infrastructure.EventHandlers;

public class TestEventHandler : IEventHandler<TestEventMessage>
{
    public Task Consume(ConsumeContext<TestEventMessage> context)
    {
        throw new NotImplementedException();
    }
}