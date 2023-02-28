﻿using App.Infrastructure.Commands;
using App.Services.Games.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Games.Infrastructure.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task Consume(ConsumeContext<TestCommandMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}