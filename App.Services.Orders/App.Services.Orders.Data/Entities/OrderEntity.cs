using App.Data;
using App.Data.Attributes;

namespace App.Services.Orders.Data.Entities
{
    [CollectionDefinition(nameof(OrderEntity))]
    public class OrderEntity : BaseEntity
    {
        public string UserId { get; set; }

        public decimal Total { get; set; }

        public OrderLine[] OrderLines { get; set; }

        public class OrderLine
        {
            public string TicketId { get; set; }
            public string ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}
