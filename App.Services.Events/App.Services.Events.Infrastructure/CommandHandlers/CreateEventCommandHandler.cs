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
    public class CreateEventCommandHandler : ICommandHandler<CreateEventCommandMessage>
    {
        private IEntityDataService _entityDataService;

        private IPublishEndpoint _publishEndpoint;

        public CreateEventCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CreateEventCommandMessage> context)
        {
            var message = context.Message;

            var entity = new EventEntity
            {
                EndDate = message.EndDate,
                EventName = message.EventName,
                Location = message.Location,
                StartDate = message.StartDate,
                Tournaments = message.Tournaments,
            };

            await _entityDataService.SaveEntity(entity);

            await _publishEndpoint.Publish(new EventCreatedEventMessage
            {
                Id = entity.Id
            });
        }
    }
}
