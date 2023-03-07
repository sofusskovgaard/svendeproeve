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

        await _entityDataService.Update<TeamEntity>(
            filter => filter.Eq(entity => entity.OrganizationId, message.Id),
            builder => builder.Unset(entity => entity.OrganizationId));
    }
}