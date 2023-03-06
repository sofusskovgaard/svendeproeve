using App.Infrastructure.Commands;
using App.Services.Organizations.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Organizations.Infrastructure.CommandHandlers;

public class TestCommandHandler : ICommandHandler<TestCommandMessage>
{
    public Task Consume(ConsumeContext<TestCommandMessage> context)
    {
        throw new NotImplementedException();
    }
}