using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Organizations.Data.Entities;
using App.Services.Teams.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Organizations.Infrastructure.EventHandlers
{
    public class TeamDeletedEventHandler : IEventHandler<TeamDeletedEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public TeamDeletedEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<TeamDeletedEventMessage> context)
        {
            var organizations = await _entityDataService.ListEntities<OrganizationEntity>(filter => filter.AnyStringIn(entity => entity.TeamIds, context.Message.Id));

            foreach (var organization in organizations)
            {
                organization.TeamIds = organization.TeamIds.Where(t => t != context.Message.Id).ToArray();

                var updateDefinition = new UpdateDefinitionBuilder<OrganizationEntity>().Set(entity => entity.TeamIds, organization.TeamIds);

                await _entityDataService.Update<OrganizationEntity>(filter => filter.Eq(entity => entity.Id, organization.Id), _ => updateDefinition);
            }
        }
    }
}
