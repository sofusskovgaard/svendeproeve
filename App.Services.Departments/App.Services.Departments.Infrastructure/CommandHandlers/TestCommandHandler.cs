using App.Infrastructure.Commands;
using App.Services.Departments.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Departments.Infrastructure.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task Consume(ConsumeContext<TestCommandMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}