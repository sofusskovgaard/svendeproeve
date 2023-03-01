﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Infrastructure.Commands
{
    public class BookTicketsCommandMessage
    {
        public string UserId { get; set; }
        public TicketOrder[] TicketOrders { get; set; }

        public class TicketOrder
        {
            public string ProductId { get; set; }
            public string Recipient { get; set; }
        }
    }

    
}