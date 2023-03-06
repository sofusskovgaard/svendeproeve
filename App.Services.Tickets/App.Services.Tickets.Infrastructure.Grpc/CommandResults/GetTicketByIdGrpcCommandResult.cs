using App.Common.Grpc;
using App.Services.Tickets.Common.Dtos;
using ProtoBuf;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetTicketByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public TicketDto Ticket { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}