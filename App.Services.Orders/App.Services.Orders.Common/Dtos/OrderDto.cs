using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Common.Dtos
{
    [ProtoContract]
    public class OrderDto
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string UserId { get; set; }

        [ProtoMember(3)]
        public double Total { get; set; }

        [ProtoMember(4)]
        public string[] TicketIds { get; set; }
    }
}
