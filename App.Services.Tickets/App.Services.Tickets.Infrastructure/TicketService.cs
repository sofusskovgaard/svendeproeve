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
using App.Services.Tickets.Common;

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
            var tickets = message.TicketOrders.Select(ticket => new TicketEntity
            {
                BuyerId = message.UserId,
                ProductId = ticket.ProductId,
                Recipient = ticket.Recipient
            }).ToList();

            await _entityDataService.SaveEntities(tickets);

            await _publishEndpoint.Publish(new TicketsBookedEventMessage
            {
                UserId = message.UserId,
                Tickets = tickets.Select(x => new TicketsBookedEventMessage.Ticket { ProductId = x.ProductId, TicketId = x.Id! }).ToArray()
            });

            await _publishEndpoint.Publish(new TicketStaleCheckCommandMessage{ Tickets = tickets.Select(t => t.Id).ToArray() },
                context => context.Delay = TimeSpan.FromSeconds(15));
        }
    }
}
