using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Events.Data.Entities;
using App.Services.Events.Infrastructure.Commands;
using App.Services.Events.Infrastructure.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Events.Infrastructure.CommandHandlers
{
    public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteEventCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<DeleteEventCommandMessage> context)
        {
            var message = context.Message;

            await _entityDataService.Delete<EventEntity>(filter => filter.Eq(entity => entity.Id, message.Id));

            await _publishEndpoint.Publish(new EventDeletedEventMessage { Id =  message.Id });
        }
    }
}
