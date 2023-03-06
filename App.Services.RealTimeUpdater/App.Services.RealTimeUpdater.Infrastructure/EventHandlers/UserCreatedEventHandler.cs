using App.Infrastructure.Events;
using App.Services.Users.Infrastructure.Events;
using MassTransit;

namespace App.Services.RealTimeUpdater.Infrastructure.EventHandlers;

public class UserCreatedEventHandler : IEventHandler<UserCreatedEventMessage>
{
    public Task Consume(ConsumeContext<UserCreatedEventMessage> context)
    {
        throw new NotImplementedException();
    }
}