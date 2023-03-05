using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Games.Data.Entities;
using App.Services.Games.Infrastructure.Commands;
using App.Services.Games.Infrastructure.Events;
using MassTransit;

namespace App.Services.Games.Infrastructure.CommandHandlers;

public class DeleteGameCommandHandler : ICommandHandler<DeleteGameCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteGameCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        this._entityDataService = entityDataService;
        this._publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeleteGameCommandMessage> context)
    {
        var message = context.Message;

        var game = await this._entityDataService.GetEntity<GameEntity>(message.Id);

        await this._entityDataService.Delete(game);

        await this._publishEndpoint.Publish(new GameDeletedEventMessage { Id = message.Id });
    }
}