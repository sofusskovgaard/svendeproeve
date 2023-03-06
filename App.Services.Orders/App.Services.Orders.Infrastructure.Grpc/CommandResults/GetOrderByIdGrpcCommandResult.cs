using App.Common.Grpc;
using App.Services.Orders.Common.Dtos;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetOrderByIdGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public OrderDto Order { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}