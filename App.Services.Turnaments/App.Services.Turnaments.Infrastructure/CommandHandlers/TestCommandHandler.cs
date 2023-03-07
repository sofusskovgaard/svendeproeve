using App.Infrastructure.Commands;
using App.Services.Turnaments.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Turnaments.Infrastructure.CommandHandlers;

public class TestCommandHandler : ICommandHandler<TestCommandMessage>
{
    public Task Consume(ConsumeContext<TestCommandMessage> context)
    {
        throw new NotImplementedException();
    }
}