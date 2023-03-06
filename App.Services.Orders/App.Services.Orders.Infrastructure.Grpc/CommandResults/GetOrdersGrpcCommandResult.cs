using App.Common.Grpc;
using App.Services.Orders.Common.Dtos;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetOrdersGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    public OrderDto[] Data { get; set; }
}