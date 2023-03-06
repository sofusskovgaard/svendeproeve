using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Events.Data.Entities;
using App.Services.Events.Infrastructure.Commands;
using App.Services.Events.Infrastructure.Events;
using MassTransit;

namespace App.Services.Events.Infrastructure.CommandHandlers;

public class CreateEventCommandHandler : ICommandHandler<CreateEventCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public CreateEventCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        this._entityDataService = entityDataService;
        this._publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<CreateEventCommandMessage> context)
    {
        var message = context.Message;

        var entity = new EventEntity
        {
            EndDate = message.EndDate,
            EventName = message.EventName,
            Location = message.Location,
            StartDate = message.StartDate,
            Tournaments = message.Tournaments
        };

        await this._entityDataService.SaveEntity(entity);

        await this._publishEndpoint.Publish(new EventCreatedEventMessage
        {
            Id = entity.Id
        });
    }
}