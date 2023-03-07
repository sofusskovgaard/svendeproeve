using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Organizations.Data.Entities;
using App.Services.Teams.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Organizations.Infrastructure.EventHandlers;

public class TeamDeletedEventHandler : IEventHandler<TeamDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public TeamDeletedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<TeamDeletedEventMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<OrganizationEntity>(
            filter => filter.AnyEq(entity => entity.TeamIds, message.Id),
            builder => builder.Pull(entity => entity.TeamIds, message.Id));
    }
}