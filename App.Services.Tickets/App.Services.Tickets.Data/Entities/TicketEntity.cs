using App.Data;
using App.Data.Attributes;

namespace App.Services.Tickets.Data.Entities
{
    [CollectionDefinition(nameof(TicketEntity))]

    public class TicketEntity : BaseEntity
    {
        public string ProductId { get; set; }

        public string Recipient { get; set; }

        public string Status { get; set; }

        public string OrderId { get; set; }
    }
}
