using App.Common.Grpc;
using App.Services.Tickets.Common.Records;
using ProtoBuf;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandMessages;

[ProtoContract]
public class BookTicketsGrpcCommandMessage : GrpcCommandMessage
{
    [ProtoMember(1)]
    public TicketBooking[] Bookings { get; set; }

    [ProtoMember(100)]
    public override GrpcCommandMessageMetadata? Metadata { get; set; }
}