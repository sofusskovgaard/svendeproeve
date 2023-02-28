using App.Infrastructure.Events;
using App.Services.Turnaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Turnaments.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}