using App.Infrastructure.Events;
using App.Services.Organizations.Infrastructure.Events;
using MassTransit;

namespace App.Services.Organizations.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}