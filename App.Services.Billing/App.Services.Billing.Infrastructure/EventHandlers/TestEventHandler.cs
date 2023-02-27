using App.Infrastructure.Events;
using App.Services.Billing.Infrastructure.Events;
using MassTransit;

namespace App.Services.Billing.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}