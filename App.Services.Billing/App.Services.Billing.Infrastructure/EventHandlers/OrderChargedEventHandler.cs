using System.Runtime.Serialization;
using System.Security.Cryptography;
using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Billing.Common.Constants;
using App.Services.Billing.Data.Entities;
using App.Services.Billing.Infrastructure.Events;
using MassTransit;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace App.Services.Billing.Infrastructure.EventHandlers;

public class OrderChargedEventHandler : IEventHandler<OrderChargedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public OrderChargedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<OrderChargedEventMessage> context)
    {
        var message = context.Message;

        var transactionNumber = Convert.ToHexString(RandomNumberGenerator.GetBytes(16)).ToLower();

        await _entityDataService.Update<OrderChargeEntity>(filter => filter.Eq(entity => entity.Id, message.OrderChargeId),
            builder => builder.Set(entity => entity.Status, OrderChargeStatus.Paid).Set(entity => entity.TransactionNumber, transactionNumber));

        await Task.Delay(500);

        var orderCharge = await _entityDataService.GetEntity<OrderChargeEntity>(message.OrderChargeId);

        await context.Publish(new OrderChargePaidEventMessage
        {
            OrderChargeId = message.OrderChargeId,
            OrderId = orderCharge.OrderId,
            TransactionNumber = transactionNumber
        });
    }
}