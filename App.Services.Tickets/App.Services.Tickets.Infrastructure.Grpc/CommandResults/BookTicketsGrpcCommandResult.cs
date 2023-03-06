using App.Common.Grpc;
using ProtoBuf;

namespace App.Services.Tickets.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class BookTicketsGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}