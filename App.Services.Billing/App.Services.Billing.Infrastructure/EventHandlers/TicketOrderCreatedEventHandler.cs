using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Billing.Common.Constants;
using App.Services.Billing.Data.Entities;
using App.Services.Billing.Infrastructure.Events;
using App.Services.Orders.Infrastructure.Events;
using MassTransit;

namespace App.Services.Billing.Infrastructure.EventHandlers
{
    public class TicketOrderCreatedEventHandler : IEventHandler<TicketOrderCreatedEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public TicketOrderCreatedEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<TicketOrderCreatedEventMessage> context)
        {
            // check if user has saved card.

            // if user has saved card charge their card and do no more.
            // emit PendingOrderChargeCreatedEventHandler
            // new OrderChargeEntity() Type = PENDING

            // if user does not have card create checkout session and store by order id
            // emit OrderCheckoutSessionCreatedEventMessage

            var message = context.Message;

            var orderCharge = new OrderChargeEntity()
            {
                OrderId = message.OrderId,
                Status = OrderChargeStatus.Pending,
                Amount = message.Total
            };

            await _entityDataService.SaveEntity(orderCharge);

            await Task.Delay(500);

            await context.Publish(new PendingOrderChargeCreatedEventMessage
            {
                OrderChargeId = orderCharge.Id!
            });
        }
    }
}