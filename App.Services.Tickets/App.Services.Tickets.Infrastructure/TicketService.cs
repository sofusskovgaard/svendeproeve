using App.Data.Services;
using App.Services.Tickets.Data.Entities;
using App.Services.Tickets.Infrastructure.Commands;
using App.Services.Tickets.Infrastructure.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Infrastructure
{
    public interface ITicketService
    {
        ValueTask CreateTickets(BookTicketsCommandMessage message);
    }

    public class TicketService : ITicketService
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IPublishEndpoint _publishEndpoint;

        public TicketService(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }

        public async ValueTask CreateTickets(BookTicketsCommandMessage message)
        {
            var orderOfTickets = new List<TicketEntity>();

            foreach (var item in message.TicketOrders)
            {
                var ticket = new TicketEntity
                {
                    ProductId = item.ProductId,
                    Recipient = item.Recipient,
                    Status = "Created"
                };
                orderOfTickets.Add(ticket);
            }

            await _entityDataService.SaveEntities(orderOfTickets);

            var eventMessage = new TicketsBookedEventMessage
            {
                UserId = message.UserId,
                Tickets = orderOfTickets.Select((x) => new TicketsBookedEventMessage.Ticket { ProductId = x.ProductId, TicketId = x.Id }).ToArray()
            };

            await _publishEndpoint.Publish(eventMessage);
        }
    }
}
