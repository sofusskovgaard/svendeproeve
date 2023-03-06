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
    [IndexDefinition("product")]
    [IndexDefinition("buyer")]
    [IndexDefinition("status")]
    [IndexDefinition("order")]
    [CollectionDefinition(nameof(TicketEntity))]

    public class TicketEntity : BaseEntity
    {
        [IndexedProperty("product")]
        public string ProductId { get; set; }

        [IndexedProperty("buyer")]
        public string BuyerId { get; set; }

        public string Recipient { get; set; }

        [IndexedProperty("status")]
        public string Status { get; set; } = TicketStatus.Booked;

        [IndexedProperty("order")]
        public string? OrderId { get; set; }
    }
}
