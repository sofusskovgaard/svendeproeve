using App.Infrastructure.Commands;
using App.Services.Authentication.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Authentication.Infrastructure.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task Consume(ConsumeContext<TestCommandMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}