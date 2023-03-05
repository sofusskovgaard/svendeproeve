using App.Infrastructure.Events;
using MassTransit;
using RealTimeUpdater.Infrastructure.Events;

namespace RealTimeUpdater.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}