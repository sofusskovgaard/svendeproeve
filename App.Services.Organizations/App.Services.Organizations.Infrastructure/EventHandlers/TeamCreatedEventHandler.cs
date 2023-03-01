using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Organizations.Data.Entities;
using App.Services.Teams.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Organizations.Infrastructure.EventHandlers
{
    public class TeamCreatedEventHandler : IEventHandler<TeamCreatedEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public TeamCreatedEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<TeamCreatedEventMessage> context)
        {
            var organization = await _entityDataService.GetEntity<OrganizationEntity>(context.Message.OrganizationId);

            organization.TeamIds.Append(context.Message.Id);

            var updateDefinition = new UpdateDefinitionBuilder<OrganizationEntity>().Set(entity => entity.TeamIds, organization.TeamIds);

            await _entityDataService.Update<OrganizationEntity>(filter => filter.Eq(entity => entity.Id, organization.Id), _ => updateDefinition);
        }
    }
}
