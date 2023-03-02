using App.Infrastructure.Events;
using App.Services.Orders.Infrastructure.Events;
using MassTransit;

namespace App.Services.Orders.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}