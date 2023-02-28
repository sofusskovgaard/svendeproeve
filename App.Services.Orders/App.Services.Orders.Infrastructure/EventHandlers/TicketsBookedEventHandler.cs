using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Orders.Data.Entities;
using App.Services.Orders.Infrastructure.Events;
using App.Services.Tickets.Infrastructure.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Infrastructure.EventHandlers
{
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
            var entity = new OrderEntity
            {
                UserId = context.Message.UserId,
                TicketIds = context.Message.Tickets.Select(x => x.TicketId).ToArray(),
                Total = await GetTotal(context.Message.Tickets)
            };

            await _entityDataService.SaveEntity(entity);

            TicketOrderCreatedEventMessage message = new TicketOrderCreatedEventMessage
            {
                OrderId = entity.Id,
                Tickets = entity.TicketIds,
                Total = entity.Total,
                UserId = entity.UserId,
            };

            await _publishEndpoint.Publish(message);

        }
        private async Task<double> GetTotal(TicketsBookedEventMessage.Ticket[] tickets)
        {
            //TODO: smarter code
            double total = 0;
            foreach (var ticket in tickets)
            {
                var product = await _entityDataService.GetEntity<ProductEntity>(ticket.ProductId);
                total += product.Price;
            }
            return total;
        }
    }
}
