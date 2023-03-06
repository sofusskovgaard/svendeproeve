using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class BookTicketsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public TicketOrder[] TicketOrders { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }

    [ProtoContract]
    public class TicketOrder
    {
        [ProtoMember(1)]
        public string ProductId { get; set; }

        /// <summary>
        ///     Name of the ticket holder
        /// </summary>
        [ProtoMember(2)]
        public string Recipient { get; set; }
    }
}