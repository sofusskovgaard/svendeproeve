using ProtoBuf;
using App.Common.Grpc;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandMessages
{
    [ProtoContract]
    public class BookTicketsGrpcCommandMessage : IGrpcCommandMessage
    {
        [ProtoMember(1)]
        public string UserId { get; set; }
        [ProtoMember(2)]
        public TicketOrder[] TicketOrders { get; set; }

        [ProtoContract]
        public class TicketOrder
        {
            [ProtoMember(1)]
            public string ProductId { get; set; }
            /// <summary>
            /// Name of the ticket holder
            /// </summary>
            [ProtoMember(2)]
            public string Recipient { get; set; }
        }
    }
}
