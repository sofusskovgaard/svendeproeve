using App.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Infrastructure.Events;

namespace App.Services.Orders.Infrastructure.Events
{
    public class TicketOrderCreatedEventMessage : IEventMessage
    {
        public string UserId { get; set; }

        public string OrderId { get; set; }

        public decimal Total { get; set; }

        public OrderLine[] OrderLines { get; set; }

        public class OrderLine
        {
            public string? ReferenceId { get; set; }

            public string? ReferenceType { get; set; }

            public string ProductId { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }
        }
    }
}
