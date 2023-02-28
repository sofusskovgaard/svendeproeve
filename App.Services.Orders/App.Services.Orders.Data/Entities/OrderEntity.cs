using App.Data;
using App.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Data.Entities
{
    [CollectionDefinition(nameof(OrderEntity))]
    public class OrderEntity : BaseEntity
    {
        public string UserId { get; set; }

        public double Total { get; set; }

        public string[] TicketIds { get; set; }
    }
}
