using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tournaments.Infrastructure.CommandHandlers;

public class DeleteTurnamentCommandHandler : ICommandHandler<DeleteTournamentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteTurnamentCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeleteTournamentCommandMessage> context)
    {
        var message = context.Message;

        var turnament = await _entityDataService.GetEntity<TournamentEntity>(message.Id);

        await _entityDataService.Delete(turnament);

        await _publishEndpoint.Publish(new TournamentDeletedEventMessage
        {
            Id = message.Id
        });
    }
}