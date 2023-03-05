using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Billing.Infrastructure.Events;
using App.Services.Orders.Common.Constants;
using App.Services.Orders.Data.Entities;
using App.Services.Orders.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Orders.Infrastructure.EventHandlers;

public class OrderChargePaidEventHandler : IEventHandler<OrderChargePaidEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public OrderChargePaidEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<OrderChargePaidEventMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<OrderEntity>(filter => filter.Eq(entity => entity.Id, message.OrderId),
            builder => builder.Set(entity => entity.Status, OrderStatus.Paid)
                .Set(entity => entity.ChargeId, message.OrderChargeId));

        var order = await _entityDataService.GetEntity<OrderEntity>(message.OrderId);

        foreach (var orderLine in order.OrderLines)
        {
            switch (orderLine.ReferenceType)
            {
                case ProductReferenceType.Ticket:
                    await context.Publish(new TicketOrderPaidEventMessage
                    {
                        OrderId = message.OrderId,
                        TicketId = orderLine.ReferenceId!
                    });
                    break;
            }
        }
    }
}