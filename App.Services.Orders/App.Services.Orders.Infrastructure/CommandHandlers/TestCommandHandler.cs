using App.Infrastructure.Commands;
using App.Services.Orders.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Orders.Infrastructure.CommandHandlers;

public class TestCommandHandler : ICommandHandler<TestCommandMessage>
{
    public Task Consume(ConsumeContext<TestCommandMessage> context)
    {
        throw new NotImplementedException();
    }
}