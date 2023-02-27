﻿using App.Infrastructure.Grpc;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class BookTicketsGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string UserId { get; set; }
        [ProtoMember(2)]
        public TicketOrder[] TicketOrders { get; set; }

        public class TicketOrder
        {
            public string ProductId { get; set; }
            /// <summary>
            /// Name of the ticket holder
            /// </summary>
            public string Recipient { get; set; }
        }
    }
}
