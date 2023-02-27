using App.Infrastructure.Events;
using App.Services.Tickets.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tickets.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}