using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tickets.Common;
using App.Services.Tickets.Data.Entities;
using App.Services.Tickets.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Tickets.Infrastructure.CommandHandlers;

public class TicketStaleCheckCommandHandler : ICommandHandler<TicketStaleCheckCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public TicketStaleCheckCommandHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<TicketStaleCheckCommandMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<TicketEntity>(
            filter => filter.And(
                filter.In(entity => entity.Id, message.Tickets),
                filter.Eq(entity => entity.Status, TicketStatus.Booked)
            ), builder => builder.Set(entity => entity.Status, TicketStatus.Stale));
    }
}