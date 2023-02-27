﻿using App.Infrastructure.Commands;
using App.Services.Events.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Events.Infrastructure.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task Consume(ConsumeContext<TestCommandMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}