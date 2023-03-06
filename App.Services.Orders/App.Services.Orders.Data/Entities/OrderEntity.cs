using App.Data;
using App.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Orders.Common.Constants;

namespace App.Services.Orders.Data.Entities
{
    [CollectionDefinition(nameof(OrderEntity))]
    public class OrderEntity : BaseEntity
    {
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
}
