﻿using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Orders.Infrastructure.Events;
using App.Services.Tickets.Data.Entities;
using MassTransit;

namespace App.Services.Tickets.Infrastructure.EventHandlers;

public class TicketOrderCreatedEventHandler : IEventHandler<TicketOrderCreatedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public TicketOrderCreatedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<TicketOrderCreatedEventMessage> context)
    {
        foreach (var orderLine in context.Message.OrderLines)
            await _entityDataService.Update<TicketEntity>(
                filter => filter.Eq(entity => entity.Id, orderLine.ReferenceId),
                builder => builder.Set(entity => entity.OrderId, context.Message.OrderId)
            );
    }
}