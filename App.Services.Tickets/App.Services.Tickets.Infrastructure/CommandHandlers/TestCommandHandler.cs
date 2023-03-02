using App.Infrastructure.Commands;
using App.Services.Tickets.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Tickets.Infrastructure.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task Consume(ConsumeContext<TestCommandMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}