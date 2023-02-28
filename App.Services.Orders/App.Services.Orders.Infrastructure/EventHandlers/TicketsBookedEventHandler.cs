using App.Infrastructure.Events;
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
        private readonly IOrdersService _ordersService;

        public TicketsBookedEventHandler(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        public Task Consume(ConsumeContext<TicketsBookedEventMessage> context)
        {
            _ordersService.dosomething
            return Task.CompletedTask;
        }
    }
}
