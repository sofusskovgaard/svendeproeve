using App.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Infrastructure.Events
{
    public class ProductCreatedEventMessage : IEventMessage
    {
        public string Id { get; set; }

    }
}
