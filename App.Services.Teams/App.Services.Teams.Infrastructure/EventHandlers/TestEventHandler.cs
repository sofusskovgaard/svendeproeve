using App.Infrastructure.Events;
using App.Services.Teams.Infrastructure.Events;
using MassTransit;

namespace App.Services.Teams.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}