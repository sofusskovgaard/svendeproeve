using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Organizations.Infrastructure.Events;
using App.Services.Teams.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Teams.Infrastructure.EventHandlers;

public class OrganizationDeletedEventHandler : IEventHandler<OrganizationDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public OrganizationDeletedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<OrganizationDeletedEventMessage> context)
    {
        var message = context.Message;

        var teams = await _entityDataService.ListEntities<TeamEntity>(filter =>
            filter.Eq(entity => entity.OrganizationId, message.Id));

        foreach (var team in teams)
        {
            var updateDefinition = new UpdateDefinitionBuilder<TeamEntity>().Set(entity => entity.OrganizationId, null);

            await _entityDataService.Update<TeamEntity>(filter => filter.Eq(entity => entity.Id, team.Id),
                _ => updateDefinition);
        }
    }
}