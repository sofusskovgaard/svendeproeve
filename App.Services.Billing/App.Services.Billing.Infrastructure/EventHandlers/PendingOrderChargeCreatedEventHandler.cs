using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Billing.Common.Constants;
using App.Services.Billing.Data.Entities;
using App.Services.Billing.Infrastructure.Events;
using MassTransit;

namespace App.Services.Billing.Infrastructure.EventHandlers;

public class PendingOrderChargeCreatedEventHandler : IEventHandler<PendingOrderChargeCreatedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public PendingOrderChargeCreatedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<PendingOrderChargeCreatedEventMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<OrderChargeEntity>(
            filter => filter.Eq(entity => entity.Id, message.OrderChargeId),
            builder => builder.Set(entity => entity.Status, OrderChargeStatus.Charged));

        await Task.Delay(500);

        await context.Publish(new OrderChargedEventMessage { OrderChargeId = message.OrderChargeId });
    }
}