using App.Common.Grpc;
using App.Services.Orders.Common.Dtos;
using ProtoBuf;

namespace App.Services.Orders.Infrastructure.Grpc.CommandResults;

[ProtoContract]
public class GetProductsGrpcCommandResult : IGrpcCommandResult
{
    [ProtoMember(2)]
    public ProductDto[] Products { get; set; }

    [ProtoMember(1)]
    public GrpcCommandResultMetadata Metadata { get; set; }
}