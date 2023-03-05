using App.Data;
using App.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Tickets.Common;

namespace App.Services.Tickets.Data.Entities
{
    [CollectionDefinition(nameof(TicketEntity))]

    public class TicketEntity : BaseEntity
    {
        public string ProductId { get; set; }

        public string BuyerId { get; set; }

        public string Recipient { get; set; }

        public string Status { get; set; } = TicketStatus.Booked;

        public string? OrderId { get; set; }
    }
}
