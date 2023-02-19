using App.Infrastructure.Commands;
using MassTransit;
using App.Services.Users.Infrastructure.Commands;

namespace App.Services.Users.Infrastructure.CommandHandlers;

public class TestCommandHandler : ICommandHandler<TestCommandMessage>
{
    public Task Consume(ConsumeContext<TestCommandMessage> context)
    {
        throw new NotImplementedException();
    }
}