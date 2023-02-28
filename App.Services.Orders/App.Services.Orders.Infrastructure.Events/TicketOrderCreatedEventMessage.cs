using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Infrastructure.Events
{
    public class TicketOrderCreatedEventMessage
    {
        public string UserId { get; set; }

        public string OrderId { get; set; }

        public string[] Tickets { get; set; }

        public double total { get; set; }
    }
}
