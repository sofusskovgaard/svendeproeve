using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Orders.Infrastructure.Events;
using App.Services.Tickets.Common;
using App.Services.Tickets.Data.Entities;
using App.Services.Tickets.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tickets.Infrastructure.EventHandlers
{
    public class TicketOrderPaidEventHandler : IEventHandler<TicketOrderPaidEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public TicketOrderPaidEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<TicketOrderPaidEventMessage> context)
        {
            var message = context.Message;

            await _entityDataService.Update<TicketEntity>(filter => filter.Eq(entity => entity.Id, message.TicketId),
                builder => builder.Set(entity => entity.Status, TicketStatus.Active));

            await context.Publish(new TicketActivatedEventMessage { TicketId = message.TicketId });
        }
    }
}