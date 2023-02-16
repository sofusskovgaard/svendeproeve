using MassTransit;

namespace App.Infrastructure.Events;

public interface IEventHandler<TEventMessage> : IEventHandler, IConsumer<TEventMessage>
    where TEventMessage : class, IEventMessage
{
}

public interface IEventHandler
{
}