using App.Common.Grpc;
using App.Services.Orders.Common.Dtos;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetProductsGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }

    [ProtoMember(2)]
    public ProductDto[] Data { get; set; }
}