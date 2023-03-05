using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Events.Data.Entities;
using App.Services.Events.Infrastructure.Commands;
using App.Services.Events.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Events.Infrastructure.CommandHandlers;

public class UpdateEventCommandHandler : ICommandHandler<UpdateEventCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateEventCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        this._entityDataService = entityDataService;
        this._publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<UpdateEventCommandMessage> context)
    {
        var message = context.Message;

        await this._entityDataService.Update<EventEntity>(
            filter => filter.Eq(entity => entity.Id, message.Id),
            builder => builder.Set(entity => entity.EventName, message.EventName)
                .Set(entity => entity.Location, message.Location)
                .Set(entity => entity.StartDate, message.StartDate)
                .Set(entity => entity.EndDate, message.EndDate)
        );

        var updateEvent = new EventUpdatedEventMessage
        {
            Id = message.Id
        };
        await this._publishEndpoint.Publish(updateEvent);
    }
}