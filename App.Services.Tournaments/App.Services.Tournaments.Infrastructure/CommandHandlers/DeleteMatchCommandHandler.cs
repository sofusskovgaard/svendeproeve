using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tournaments.Infrastructure.CommandHandlers;

public class DeleteMatchCommandHandler : ICommandHandler<DeleteMatchCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteMatchCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeleteMatchCommandMessage> context)
    {
        var message = context.Message;

        var result = await _entityDataService.Delete<MatchEntity>(filter => filter.Eq(entity => entity.Id, message.Id));

        if (result)
        {
            await _publishEndpoint.Publish(new MatchDeletedEventMessage{ Id = message.Id });
        }
    }
}