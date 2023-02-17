using App.Infrastructure.Commands;
using MassTransit;
using Template.Services.ServiceName.Infrastructure.Commands;

namespace Template.Services.ServiceName.Infrastructure.CommandHandlers;

public class TestCommandHandler : ICommandHandler<TestCommandMessage>
{
    public Task Consume(ConsumeContext<TestCommandMessage> context)
    {
        throw new NotImplementedException();
    }
}