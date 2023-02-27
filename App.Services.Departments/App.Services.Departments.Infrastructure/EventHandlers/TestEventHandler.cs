using App.Infrastructure.Events;
using App.Services.Departments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Departments.Infrastructure.EventHandlers
{
    public class TestEventHandler : IEventHandler<TestEventMessage>
    {
        public Task Consume(ConsumeContext<TestEventMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}