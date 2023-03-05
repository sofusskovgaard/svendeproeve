using App.Infrastructure.Commands;
using MassTransit;
using RealTimeUpdater.Infrastructure.Commands;

namespace RealTimeUpdater.Infrastructure.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task Consume(ConsumeContext<TestCommandMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}