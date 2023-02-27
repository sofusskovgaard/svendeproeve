using App.Infrastructure.Events;
using App.Services.Authentication.Infrastructure.Events;
using MassTransit;

namespace App.Services.Authentication.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}