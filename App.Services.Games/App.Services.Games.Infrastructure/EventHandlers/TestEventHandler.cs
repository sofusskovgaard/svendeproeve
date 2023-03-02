using App.Infrastructure.Events;
using App.Services.Games.Infrastructure.Events;
using MassTransit;

namespace App.Services.Games.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}