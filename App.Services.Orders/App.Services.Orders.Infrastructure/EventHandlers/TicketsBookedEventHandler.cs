﻿using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Orders.Common.Constants;
using App.Services.Orders.Data.Entities;
using App.Services.Orders.Infrastructure.Events;
using App.Services.Tickets.Infrastructure.Events;
using MassTransit;

namespace App.Services.Orders.Infrastructure.EventHandlers;

public class TicketsBookedEventHandler : IEventHandler<TicketsBookedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public TicketsBookedEventHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<TicketsBookedEventMessage> context)
    {
        var products = await _entityDataService.ListEntities<ProductEntity>();

        var entity = new OrderEntity
        {
            UserId = context.Message.UserId,
            OrderLines = context.Message.Tickets.Select(ticket => new OrderEntity.OrderLine
            {
                ReferenceId = ticket.TicketId, ReferenceType = ProductReferenceType.Ticket,
                Price = products.FirstOrDefault(x => x.Id == ticket.ProductId).Price, Quantity = 1,
                ProductId = ticket.ProductId
            }).ToArray()
        };
        entity.Total = GetTotal(entity.OrderLines);

        await _entityDataService.SaveEntity(entity);

        var message = new TicketOrderCreatedEventMessage
        {
            OrderId = entity.Id,
            OrderLines = entity.OrderLines.Select(orderLine => new TicketOrderCreatedEventMessage.OrderLine
            {
                ReferenceId = orderLine.ReferenceId, ReferenceType = ProductReferenceType.Ticket,
                Price = orderLine.Price, ProductId = orderLine.ProductId, Quantity = orderLine.Quantity
            }).ToArray(),
            Total = entity.Total,
            UserId = entity.UserId
        };

        await _publishEndpoint.Publish(message);
    }

    private decimal GetTotal(OrderEntity.OrderLine[] tickets)
    {
        return tickets.Sum(x => x.Price);
    }
}