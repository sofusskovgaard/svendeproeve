using MassTransit;

namespace App.Infrastructure.Commands;

public interface ICommandHandler<TCommandMessage> : ICommandHandler, IConsumer<TCommandMessage>
    where TCommandMessage : class, ICommandMessage
{
}

public interface ICommandHandler
{
}