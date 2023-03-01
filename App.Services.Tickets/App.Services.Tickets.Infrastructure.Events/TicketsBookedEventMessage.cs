using App.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Infrastructure.Events
{
    public class TicketsBookedEventMessage : IEventMessage
    {
        public string UserId { get; set; }
        
        public Ticket[] Tickets { get; set; }

        public class Ticket
        {
            public string TicketId { get; set; }

            public string ProductId { get; set; }
        }
    }
}
