using App.Infrastructure.Commands;
using App.Services.Teams.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Teams.Infrastructure.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task Consume(ConsumeContext<TestCommandMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}