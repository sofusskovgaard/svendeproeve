using App.Data;
using App.Data.Attributes;
using App.Services.Orders.Common.Constants;

namespace App.Services.Orders.Data.Entities;

[IndexDefinition("user")]
[CollectionDefinition(nameof(OrderEntity))]
public class OrderEntity : BaseEntity
{
    [IndexedProperty("user")]
    public string UserId { get; set; }

    public decimal Total { get; set; }

    public OrderLine[] OrderLines { get; set; }

    public string Status { get; set; } = OrderStatus.Pending;

    public string? ChargeId { get; set; }

    public class OrderLine
    {
        public string? ReferenceId { get; set; }

        public string? ReferenceType { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}