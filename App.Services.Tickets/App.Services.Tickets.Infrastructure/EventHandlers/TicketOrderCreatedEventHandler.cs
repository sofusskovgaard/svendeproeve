using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Tickets.Data.Entities;
using App.Services.Orders.Infrastructure.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Infrastructure.EventHandlers
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
            foreach (var ticket in context.Message.Tickets)
            {
                var entity = await _entityDataService.GetEntity<TicketEntity>(ticket);

                entity.OrderId = context.Message.OrderId;
                entity.Status = "Ordered";

                await _entityDataService.Update<TicketEntity>(entity);
            }
        }
    }
}
