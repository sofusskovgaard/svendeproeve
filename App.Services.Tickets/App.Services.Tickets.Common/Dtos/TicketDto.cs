using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Common.Dtos
{
    [ProtoContract]
    public class TicketDto
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string ProductId { get; set; }

        [ProtoMember(3)]
        public string Status { get; set; }

        [ProtoMember(4)]
        public string Recipient { get; set; }
    }
}
