using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Infrastructure.Commands
{
    public class CreateOrderCommandMessage
    {
        public string UserId { get; set; }

        public double Total { get; set; }

        public string[] TicketIds { get; set; }
    }
}
