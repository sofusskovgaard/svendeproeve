using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Teams.Data.Entities;
using App.Services.Teams.Infrastructure.Commands;
using App.Services.Teams.Infrastructure.Events;
using MassTransit;

namespace App.Services.Teams.Infrastructure.CommandHandlers;

public class DeleteTeamCommandHandler : ICommandHandler<DeleteTeamCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteTeamCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeleteTeamCommandMessage> context)
    {
        var message = context.Message;

        var result = await _entityDataService.Delete<TeamEntity>(filter => filter.Eq(entity => entity.Id, message.Id));

        if (result)
        {
            await _publishEndpoint.Publish(new TeamDeletedEventMessage { Id = message.Id });
        }
    }
}