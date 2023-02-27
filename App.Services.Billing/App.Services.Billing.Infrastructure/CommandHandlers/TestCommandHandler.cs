using App.Infrastructure.Commands;
using App.Services.Billing.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Billing.Infrastructure.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task Consume(ConsumeContext<TestCommandMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}