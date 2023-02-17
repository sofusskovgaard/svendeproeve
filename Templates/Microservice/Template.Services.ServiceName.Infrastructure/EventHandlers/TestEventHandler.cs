using App.Infrastructure.Events;
using MassTransit;
using Template.Services.ServiceName.Infrastructure.Events;

namespace Template.Services.ServiceName.Infrastructure.EventHandlers;

public class TestEventHandler : IEventHandler<TestEventMessage>
{
    public Task Consume(ConsumeContext<TestEventMessage> context)
    {
        throw new NotImplementedException();
    }
}