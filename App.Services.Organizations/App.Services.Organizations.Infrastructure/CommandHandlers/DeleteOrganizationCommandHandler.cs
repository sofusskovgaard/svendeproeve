using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Organizations.Data.Entities;
using App.Services.Organizations.Infrastructure.Commands;
using App.Services.Organizations.Infrastructure.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Infrastructure.CommandHandlers
{
    public class DeleteOrganizationCommandHandler : ICommandHandler<DeleteOrganizationCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteOrganizationCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<DeleteOrganizationCommandMessage> context)
        {
            var message = context.Message;

            await _entityDataService.Delete<OrganizationEntity>(filter => filter.Eq(entity => entity.Id, message.Id));

            await _publishEndpoint.Publish(new OrganizationDeletedEventMessage { Id = message.Id });
        }
    }
}
