using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tournaments.Infrastructure.CommandHandlers;

public class DeleteTournamentCommandHandler : ICommandHandler<DeleteTournamentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteTournamentCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeleteTournamentCommandMessage> context)
    {
        var message = context.Message;

        var result = await _entityDataService.Delete<TournamentEntity>(filter => filter.Eq(entity => entity.Id, message.Id));

        if (result)
        {
            await _publishEndpoint.Publish(new TournamentDeletedEventMessage{ Id = message.Id });
        }
    }
}